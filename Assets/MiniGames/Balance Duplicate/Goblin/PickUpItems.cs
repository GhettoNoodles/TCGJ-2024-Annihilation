using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    [SerializeField]
    private HingeJoint2D hands;
    
    [SerializeField]
    private float UpperAngle = 145f;

    [SerializeField]
    private Transform gaanHier;
   
    private JointAngleLimits2D handLimit;

    [SerializeField]
    private bool HoldingItem = false, InRange = false, FindPos = false;


    private RockScript ChangeBool;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Grab(Rigidbody2D ConnectedRb)
    {
        hands = gameObject.AddComponent<HingeJoint2D>();
          

        hands.connectedBody = ConnectedRb;
        
            
        hands.useLimits = true;
            
        handLimit = hands.limits;
        handLimit.max = UpperAngle;
        hands.limits = handLimit;
    }

    public void Release()
    {
        Destroy(hands);
    }
    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.W) &&
            InRange == true)
        {
           
            

            //  ChangeBool.Beingheld = true;
            //ChangeBool.gameObject.transform.SetAsFirstSibling();
           // ChangeBool.RunOnce = true;
            //Debug.Log(ChangeBool.name);

        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            //ChangeBool.GetComponent<RockScript>().Beingheld = false;
          
            
            //ChangeBool.RunOnce = true;
            
        }
    }


}
