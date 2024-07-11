using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    [SerializeField]
    private HingeJoint2D hands;

    [SerializeField]
    private Rigidbody2D ConnectedRb;
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

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.W) &&
            InRange == true)
        {
           
            hands = gameObject.AddComponent<HingeJoint2D>();
          

            hands.connectedBody = ConnectedRb;

            ///ChangeBool = ConnectedRb.gameObject.GetComponent<RockScript>();
            
            hands.useLimits = true;
            
            handLimit = hands.limits;
            handLimit.max = UpperAngle;
            hands.limits = handLimit;

            //  ChangeBool.Beingheld = true;
            //ChangeBool.gameObject.transform.SetAsFirstSibling();
           // ChangeBool.RunOnce = true;
            //Debug.Log(ChangeBool.name);

        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            //ChangeBool.GetComponent<RockScript>().Beingheld = false;
          
            
            //ChangeBool.RunOnce = true;
            Destroy(hands);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Rock"))
        {
            InRange = true;
            ConnectedRb = collision.GetComponent<Rigidbody2D>();
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Rock"))
        {
            InRange = false;
            ConnectedRb = null;
           
        }

       
    }
}
