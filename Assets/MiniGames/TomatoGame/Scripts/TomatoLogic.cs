using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoLogic : MonoBehaviour
{
    public float health;

    public Sprite[] faceSprite;
    public float[] changePoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        health += collision.transform.GetComponent<Rigidbody2D>().velocity.y;

        if (health < 0)
        {
            if (collision.transform.tag == "Player1") FistScript.Instance.Player1_score++;
            else FistScript.Instance.Player2_score++;

            TomatoManager.Instance.SpawnTomato(transform);
            Destroy(gameObject);
        }

    }

    private void ChangeFace()
    {

    }
}
