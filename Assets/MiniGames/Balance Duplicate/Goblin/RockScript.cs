using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{

    public bool Beingheld = false, RunOnce = false;
    

    [SerializeField]
    private CircleCollider2D circle;

    [SerializeField]
    private RockScript rockScript;

    [SerializeField]
    public GameObject MainSibling;

    public ColliderState State;
    


    [SerializeField]
    private FixedJoint2D[] fixedJoint2s;

    // Start is called before the first frame update

    private void Awake()
    {
        State = ColliderState.S_notbeingHeld;
        RunOnce = false;

      //  MainSibling = transform.parent.GetChild(0).gameObject;
    }


    void Start()
    {
        circle = gameObject.GetComponent<CircleCollider2D>();
     
        //fixedJoint2s() = gameObject.GetComponents<FixedJoint2D>();
        fixedJoint2s = gameObject.GetComponents<FixedJoint2D>();
    }



    public enum ColliderState
    {
        S_beingHeld,
        S_notbeingHeld
    }
    // Update is called once per frame
    void Update()
    {
        //if (Beingheld == true)
        //{
        //    circle.enabled = true;
        //    box.enabled = false;
        //    Debug.Log("fok");

        //    if (fixedJoint2s != null)
        //    {
        //        foreach (FixedJoint2D joint in fixedJoint2s)
        //        {
        //            Changebool = joint.connectedBody.gameObject.GetComponent<RockScript>();

        //            if (Changebool.Beingheld == false)
        //            {

        //                Changebool.Beingheld = true;


        //            }

        //        }
        //    }
        //}

        //else
        //{
        //    circle.enabled = false;
        //    box.enabled = true;

        //    if (fixedJoint2s != null) ;
        //    {


        //        foreach (FixedJoint2D joint in fixedJoint2s)
        //        {
        //            Changebool = joint.connectedBody.gameObject.GetComponent<RockScript>();

        //        }
        //    }

        //}

        if (fixedJoint2s.Length> 0)
        {
            MainSibling = transform.parent.GetChild(0).gameObject;
            State = MainSibling.GetComponent<RockScript>().State;
        }
       
        
        switch (State)
        {
            case ColliderState.S_beingHeld:
              //  circle.enabled = true;
         
                //if (fixedJoint2s != null)
                //{
                    //foreach (FixedJoint2D joint in fixedJoint2s)
                    //{
                    //    Changebool = joint.connectedBody.gameObject.GetComponent<RockScript>();

                    //    if (Changebool.Beingheld == false)
                    //    {
                    //        Changebool.State = ColliderState.S_beingHeld;
                    //        Changebool.Beingheld = true;

                    //    }

                  
                    
                    
                    //for (int i = 0; i < fixedJoint2s.Length; i++)
                    //{
                    //    rockScript = fixedJoint2s[i].connectedBody.gameObject.GetComponent<RockScript>();

                    //    if (rockScript.Beingheld == false)
                    //    {
                    //        rockScript.State = ColliderState.S_beingHeld;
                    //        rockScript.Beingheld = true;
                    //    }
                    //}

                    //}

                    //RunOnce = false;
                

        break;
            case ColliderState.S_notbeingHeld:
              //  circle.enabled = false;
              

                //if (fixedJoint2s != null)
                //{
                //    foreach (FixedJoint2D joint in fixedJoint2s)
                //    {
                //        rockScript = joint.connectedBody.gameObject.GetComponent<RockScript>();

                //        if (rockScript.Beingheld == true)
                //        {
                //            rockScript.State = ColliderState.S_notbeingHeld;
                //            rockScript.Beingheld = false;

                //        }

                //    }
                //    RunOnce = false;
                //}

              
                
                
                //for (int i = 0; i < fixedJoint2s.Length; i++)
                //{
                //    rockScript = fixedJoint2s[i].connectedBody.gameObject.GetComponent<RockScript>();

                //    if (rockScript.Beingheld == true)
                //    {
                //        rockScript.State = ColliderState.S_notbeingHeld;
                //        rockScript.Beingheld = false;
                //    }
                //}

                break;
        }
    }
}