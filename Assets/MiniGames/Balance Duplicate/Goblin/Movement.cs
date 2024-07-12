using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 5f, jumpHeight = 5f;

    public bool canJump = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.position = rb.position + Vector2.right * speed * Time.deltaTime;
          //  this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.position = rb.position + Vector2.left * speed * Time.deltaTime;
          //  this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) &&
            canJump == true)
        {
            rb.AddForce(jumpHeight * Vector2.up);
            canJump = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "BodyPart")
        {
            canJump = true;
        }
    }
}
