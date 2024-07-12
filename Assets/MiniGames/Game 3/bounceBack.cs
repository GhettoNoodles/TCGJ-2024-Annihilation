using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceBack : MonoBehaviour
{
    public GameObject play_one, play_two, crocodileClosed;
    public float knockbackForce = 10.0f;
    public float stunDuration = 1.0f;
    public playerScript PlayerScript;

    public CrocodileMouthController mouthTimer; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject ==  play_one || collider.gameObject == play_two)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockbackDirection = (collider.transform.position - crocodileClosed.transform.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                collider.GetComponent<playerScript>().StunPlayer(stunDuration);
                PlayerScript = collider.GetComponent<playerScript>();
                PlayerScript.dropWhenStun();
            }
        }
    }
}
