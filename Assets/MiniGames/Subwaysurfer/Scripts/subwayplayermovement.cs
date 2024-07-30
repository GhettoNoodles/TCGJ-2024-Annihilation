using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subwayplayermovement : MonoBehaviour
{
    [SerializeField]
    private Input_Manager.PlayerNumber player;

    public Rigidbody2D rb;
    public float NewSpeed;
    public Transform[] lanes;
    private Transform target;
    [SerializeField]
    private int indextarget;
    public subwayplayermovement enemymove;
    private bool collide = false;
    private bool canMove = true;
    private float moveCooldown = 0.5f; // Adjust this value as needed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (lanes != null && lanes.Length > 0)
        {
            target = lanes[indextarget];
        }
    }

    private void FixedUpdate()
    {
        target = lanes[indextarget];
        rb.position = Vector3.MoveTowards(transform.position, target.position, NewSpeed * Time.deltaTime);

        if (canMove)
        {
            Vector2 stickInput = Input_Manager.Instance.Get_Stick(player);
            if (stickInput.x < 0)
            {
                MoveLeft();
                Debug.Log(player + " " + "left");
                
            }
            else if (stickInput.x > 0)
            {
                MoveRight();
                Debug.Log(player + " " + "right");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit something
     
    }

    private void MoveRight()
    {
        collide = false;
        if (enemymove.indextarget == indextarget+1)
        {
            collide = true;
        }
        if ((indextarget + 1 <= 4) && (!collide || enemymove.indextarget + 1 <= 4))
        {
            if (collide)
            {
                // enemymove.indextarget++;
                Debug.Log(player);
                enemymove.pushplayer(true);
            }
            indextarget++;
           
            StartCoroutine(MoveCooldown());
        }
    }

    private void MoveLeft()
    {
        collide = false;
        if (enemymove.indextarget == indextarget-1)
        {
            collide = true;
        }
        if ((indextarget - 1 >= 0) && (!collide || enemymove.indextarget - 1 >= 0))
        {
            if (collide)
            {
                //enemymove.indextarget--;
                Debug.Log(player);
               enemymove.pushplayer(false);
            }
            indextarget--;
           
            StartCoroutine(MoveCooldown());
        }
    }

    private IEnumerator MoveCooldown()
    {
        canMove = false;
        yield return new WaitForSeconds(moveCooldown);
        canMove = true;
    }

    void Update()
    {
        // Any additional update logic if needed
    }

    public void pushplayer(bool pushright)
    {
        if (pushright)
            indextarget++;
        else
            indextarget--;
    }
}
