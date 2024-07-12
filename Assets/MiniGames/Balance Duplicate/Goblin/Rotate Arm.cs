using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    [SerializeField]
    private HingeJoint2D ShoulderHinge;
    [SerializeField]
    private JointMotor2D ShoulderMotor;


    [SerializeField]
    public float MaxPower = 300f;
    public float MinPower = 50f;

    [SerializeField]
    public float CurrentPower = 100f;

    [SerializeField]
    private float ChangeValue = 50f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ShoulderHinge = gameObject.GetComponent<HingeJoint2D>();
        ShoulderMotor = ShoulderHinge.motor;
        ShoulderMotor.motorSpeed = 0f;
        ShoulderHinge.motor = ShoulderMotor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)
            && Input.GetMouseButton(1) == false)
        {
            ShoulderMotor.motorSpeed = CurrentPower * -1;
            ShoulderHinge.motor = ShoulderMotor;
        }

        else if (Input.GetMouseButton(1)
            && Input.GetMouseButton(0) == false)
        {
            ShoulderMotor.motorSpeed = CurrentPower;
            ShoulderHinge.motor = ShoulderMotor;
        }

        else if (Input.GetMouseButton(0) == false
            && Input.GetMouseButton(1) == false)
        {
            ShoulderMotor.motorSpeed = 0f;
            ShoulderHinge.motor = ShoulderMotor;
        }

        

        if (Input.mouseScrollDelta.y > 0)
        {
            CurrentPower += ChangeValue;
            
            if (CurrentPower >= MaxPower)
            {
                CurrentPower = MaxPower;
            }
        }

        else if (Input.mouseScrollDelta.y < 0)
        {
            CurrentPower -= ChangeValue;

            if (CurrentPower <= MinPower)
            {
                CurrentPower = MinPower;
            }
        }
    }

    
}
