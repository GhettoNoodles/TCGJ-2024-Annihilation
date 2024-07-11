using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTest : MonoBehaviour
{
    [SerializeField] private Image L1;
    [SerializeField] private Image L2;
    [SerializeField] private Image L_Left;
    [SerializeField] private Image L_Right;
    [SerializeField] private Image L_up;
    [SerializeField] private Image L_Down;
    [SerializeField] private Image R1;
    [SerializeField] private Image R2;
    [SerializeField] private Image R_Left;
    [SerializeField] private Image R_Right;
    [SerializeField] private Image R_Up;
    [SerializeField] private Image R_Down;

    [SerializeField] private float LHorizontal;
    [SerializeField] private float LVertical;
    [SerializeField] private float RHorizontal;
    [SerializeField] private float RVertical;

    [SerializeField] private Color off;
    [SerializeField] private Color on;
    
    void Start()
    {
        on= new Color(1, 1, 1, 1);
        off = new Color(1, 1, 1, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        L1.color = Input.GetButton("P1_Action") ? on : off;
        R1.color = Input.GetButton("P2_Action") ? on : off;
        
        var LHold = Input.GetAxis("P1_Hold");
        var RHold = Input.GetAxis("P2_Hold");
        L2.color = LHold>0 ? on : off;
        R2.color = RHold>0 ? on : off;
        
        LHorizontal = Input.GetAxis("P1_Horizontal");
        LVertical = Input.GetAxis("P1_Vertical");
        RHorizontal = Input.GetAxis("P2_Horizontal");
        RVertical = Input.GetAxis("P2_Vertical");
        if (LHorizontal>0.01)
        {
            L_Left.color = off;
            L_Right.color = on;
        }
        else if (LHorizontal<-0.01)
        {
            L_Left.color = on;
            L_Right.color = off;
        }
        else
        {
            L_Left.color = off;
            L_Right.color = off;
        }
        
        if (LVertical>0.01)
        {
            L_Down.color = off;
            L_up.color = on;
        }
        else if (LVertical<-0.01)
        {
            L_Down.color = on;
            L_up.color = off;
        }
        else
        {
            L_Down.color = off;
            L_up.color = off;
        }
        
        if (RHorizontal>0.01)
        {
            R_Left.color = off;
            R_Right.color = on;
        }
        else if (RHorizontal<-0.01)
        {
            R_Left.color = on;
            R_Right.color = off;
        }
        else
        {
            R_Left.color = off;
            R_Right.color = off;
        }
        
        if (RVertical>0.01)
        {
            R_Down.color = off;
            R_Up.color = on;
        }
        else if (RVertical<-0.01)
        {
            R_Down.color = on;
            R_Up.color = off;
        }
        else
        {
            R_Down.color = off;
            R_Up.color = off;
        }
    }
}
