using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subwayplayermovement : MonoBehaviour
{

 
    [SerializeField] private Input_Manager inputman;
    public Input_Manager.PlayerNumber playernum;
    public Rigidbody2D rb;
    public float NewSpeed;
    public Transform[] lanes;
    private Transform target;
    public int indextarget;
    public subwayplayermovement enemymove;
    bool collide = false;

    void Start()
    {
       /* inputman = Input_Manager.Instance;
        if (inputman == null)
        {
            Debug.LogError("Input_Manager.Instance is null!");
        }*/

        rb = GetComponent<Rigidbody2D>();
  

        if (lanes != null && lanes.Length > 0)
        {
            target = lanes[indextarget];
            Debug.Log("Target initialized to lane " + indextarget);
        }
       
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Target is null!");
            return;
        }

        rb.position = Vector3.MoveTowards(transform.position, target.position, NewSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            //hit something
            Debug.Log(gameObject.name + " collided with an obstacle!");
    }
    private void MoveRight()
    {
        if (enemymove.indextarget - 1 == indextarget)
        {
            collide = true;
        }
        if ((indextarget + 1 <= 4) && (!collide || enemymove.indextarget + 1 <= 4))
        {
            indextarget++;
            target = lanes[indextarget];
            if (collide)
            {
                enemymove.indextarget++;
            }

        }
        collide = false;
    }

    private void MoveLeft()
    {
        if (enemymove.indextarget - 1 == indextarget)
        {
            collide = true;
        }
        if ((indextarget - 1 >= 0) && (!collide || enemymove.indextarget - 1 >= 0))
        {
            indextarget--;
            target = lanes[indextarget];
            if (collide)
            {
                enemymove.indextarget--;
            }

        }
        collide = false;

    }

    void Update()
    {
        if (inputman == null)
        {
            return;
        }

        var velocity = inputman.Get_Stick(playernum);
        if (velocity.magnitude != 0)
        {
            Debug.Log(velocity);
        }

        if (rb != null)
        {
            rb.velocity = velocity.normalized * NewSpeed;
        }
    }
}
