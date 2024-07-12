using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoLogic : MonoBehaviour
{
    public float health;
    public float destroyTime;
    public SpriteRenderer spriteRenderer;

    public Sprite[] faceSprite;
    public float[] changePoint;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        health += collision.transform.GetComponent<Rigidbody2D>().velocity.y;
        ChangeFace();

        if (health <= 0)
        {
            if (collision.transform.tag == "Player1") FistScript.Instance.Player1_score++;
            else FistScript.Instance.Player2_score++;

            TomatoManager.Instance.SpawnTomato(transform);
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(DestroyTomato());
        }
    }

    private void ChangeFace()
    {
        if (health <= changePoint[3]) spriteRenderer.sprite = faceSprite[3];
        else if (health <= changePoint[2]) spriteRenderer.sprite = faceSprite[2];
        else if (health <= changePoint[1]) spriteRenderer.sprite = faceSprite[1];
        else if (health <= changePoint[0]) spriteRenderer.sprite = faceSprite[0];
    }

    private IEnumerator DestroyTomato()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
