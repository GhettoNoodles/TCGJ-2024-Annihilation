using System.Collections;
using UnityEngine;

public class CrocodileMouthController : MonoBehaviour
{
    public GameObject crocodileClosed; 
    public GameObject crocodileOpen; 
    [SerializeField] float openDuration = 100.0f; 
    [SerializeField] float closedDuration = 1.0f; 
    public float knockbackForce = 10.0f; 
    public float stunDuration = 1.0f; 
    public GameObject playerOne;
    public GameObject playerTwo;

    private void Start()
    {
        StartCoroutine(OpenCloseMouth());
    }

    private IEnumerator OpenCloseMouth()
    {
        while (true)
        {
            // Open mouth
            crocodileClosed.SetActive(false);
            crocodileOpen.SetActive(true);
            yield return new WaitForSeconds(openDuration);

            // Close mouth
            crocodileOpen.SetActive(false);
            crocodileClosed.SetActive(true);
            ApplyKnockbackToPlayers();
            yield return new WaitForSeconds(closedDuration);
        }
    }

    private void ApplyKnockbackToPlayers()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(crocodileClosed.transform.position, crocodileClosed.GetComponent<Collider2D>().bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject ==  playerOne || collider.gameObject == playerTwo)
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDirection = (collider.transform.position - crocodileClosed.transform.position).normalized;
                    rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                    collider.GetComponent<playerScript>().StunPlayer(stunDuration);
                }
            }
        }
    }
}