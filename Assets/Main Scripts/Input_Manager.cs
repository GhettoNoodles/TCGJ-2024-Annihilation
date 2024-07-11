using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Input_Manager : MonoBehaviour
{
    private float L_Horizontal;
    private float L_Vertical;
    private float R_Horizontal;
    private float R_Vertical;
    private float L_Hold;
    private float R_Hold;
    private bool L_Action;
    private bool R_Action;
    
    public enum PlayerNumber
    {
        P1,
        P2
    }
    
    public static Input_Manager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Input");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        L_Hold = Input.GetAxis("P1_Hold");
        R_Hold = Input.GetAxis("P2_Hold");

        L_Horizontal = Input.GetAxis("P1_Horizontal");
        L_Vertical = Input.GetAxis("P1_Vertical");
        R_Horizontal = Input.GetAxis("P2_Horizontal");
        R_Vertical = Input.GetAxis("P2_Vertical");
    }

    public bool Get_Action(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return Input.GetButton("P1_Action");
        }

        return Input.GetButton("P2_Action");
    }

    public bool Get_Action_Tap(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return Input.GetButtonDown("P1_Action");
        }

        return Input.GetButtonDown("P2_Action");
    }

    public bool Get_Hold(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return L_Hold > 0;
        }

        return R_Hold > 0;
    }

    public Vector2 Get_Stick(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return new Vector2(L_Horizontal, L_Vertical);
        }

        return new Vector2(R_Horizontal, R_Vertical);
    }
}