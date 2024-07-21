using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Input_Manager : MonoBehaviour
{
    private float L_Horizontal;
    private float L_Vertical;
    private float R_Horizontal;
    private float R_Vertical;
    private float L_Hold;
    private float R_Hold;

    private bool L_holdToggle;
    private bool R_holdToggle;
    [SerializeField] private PlayerInput input;
    [SerializeField] private InputActionReference L_Hold_Ref;
    [SerializeField] private InputActionReference R_Hold_Ref;
    [SerializeField] private InputActionReference L_Hold_Tap_Ref;
    [SerializeField] private InputActionReference R_Hold_Tap_Ref;
    [SerializeField] private InputActionReference L_Stick_Ref;
    [SerializeField] private InputActionReference R_Stick_Ref;
    [SerializeField] private InputActionReference L_Action_Ref;
    [SerializeField] private InputActionReference L_Action_Tap_Ref;
    [SerializeField] private InputActionReference R_Action_Tap_Ref;
    [SerializeField] private InputActionReference R_Action_Ref;


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
        L_Hold = L_Hold_Ref.action.ReadValue<float>();
        R_Hold = R_Hold_Ref.action.ReadValue<float>();
        if (L_Hold_Tap_Ref.action.WasPressedThisFrame())
        {
            L_holdToggle = !L_holdToggle;
        }

        if (R_Hold_Tap_Ref.action.WasPressedThisFrame())
        {
            R_holdToggle = !R_holdToggle;
        }

        L_Horizontal = L_Stick_Ref.action.ReadValue<Vector2>().x;
        R_Horizontal = R_Stick_Ref.action.ReadValue<Vector2>().x;
        L_Vertical = L_Stick_Ref.action.ReadValue<Vector2>().y;
        R_Vertical = R_Stick_Ref.action.ReadValue<Vector2>().y;
    }

    public bool Get_Action(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return L_Action_Ref.action.IsPressed();
        }

        return R_Action_Ref.action.IsPressed();
    }

    public bool Get_Action_Tap(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return L_Action_Tap_Ref.action.WasPressedThisFrame();
        }

        return R_Action_Tap_Ref.action.WasPressedThisFrame();
    }

    public bool Get_Hold(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            if (input.currentControlScheme == "Keyboard")
            {
                return L_holdToggle;
            }

            return L_Hold > 0;
        }

        if (input.currentControlScheme == "Keyboard")
        {
            return R_holdToggle;
        }

        return R_Hold > 0;
    }

    public bool Get_Action_Release(PlayerNumber player)
    {
        if (player == PlayerNumber.P1)
        {
            return L_Action_Ref.action.WasReleasedThisFrame();
        }

        return R_Action_Ref.action.WasReleasedThisFrame();
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