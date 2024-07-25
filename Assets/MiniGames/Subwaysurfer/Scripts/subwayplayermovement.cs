using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subwayplayermovement : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField]private Input_Manager inputman;
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
        inputman = Input_Manager.Instance;
      //  rb = this.GetComponent<Rigidbody2D>();
        

    }
    private void FixedUpdate()
    {
       rb.position = Vector3.MoveTowards(transform.position, target.position, NewSpeed * Time.deltaTime);
        //If pressed left
        MoveLeft();
        //if pressed right 
        MoveRight();
        //if press jump 
    }

    private void MoveRight()
    {
        if(enemymove.indextarget-1==indextarget)
        {
            collide = true;
        }
        if ((indextarget + 1 <= 4) && (!collide || enemymove.indextarget+1<=4))
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

    // Update is called once per frame
    void Update()
    {
       
       
        var velocity = inputman.Get_Stick(playernum);
        if (velocity.magnitude != 0)
        {
            Debug.Log(velocity);

        }
       

        rb.velocity = velocity.normalized * NewSpeed;

        /*  if (NewSpeed < MaxSpeed)
          {
              rb.velocity = velocity.normalized * NewSpeed;
          }

          else if (NewSpeed >= MaxSpeed)
          {
              rb.velocity = velocity.normalized * MaxSpeed;
          }*/


    }


}
