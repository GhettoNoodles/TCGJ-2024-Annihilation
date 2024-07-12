using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Balance_Player : MonoBehaviour
{
    private Input_Manager _inputManager;
    [SerializeField] private PickUpItems pickup;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private GameObject rockObject;
    [SerializeField] private Rigidbody2D handrb;
    [SerializeField] private float speed;
    [SerializeField] private float power;
    [SerializeField] private HingeJoint2D ShoulderHinge;
    private JointMotor2D ShoulderMotor;
    private GameObject currentRock;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        ShoulderMotor = ShoulderHinge.motor;
        ShoulderMotor.motorSpeed = 0f;
        ShoulderHinge.motor = ShoulderMotor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
              Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)))
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        var velocity = _inputManager.Get_Stick(playerNumber).x;
        rb.AddForce(new Vector2(velocity*speed,0));
        if (_inputManager.Get_Action_Tap(playerNumber))
        {
            currentRock = Instantiate(rockObject, handrb.position, quaternion.identity);
            currentRock.GetComponent<CircleCollider2D>().enabled = false;
            pickup.Grab(currentRock.GetComponent<Rigidbody2D>());
            currentRock.GetComponent<Rigidbody2D>().mass = 1;
        }

        if (_inputManager.Get_Action(playerNumber))
        {
            if (playerNumber ==Input_Manager.PlayerNumber.P1)
            {
                ShoulderMotor.motorSpeed = power ;
            }
            else
            {
                ShoulderMotor.motorSpeed = power *-1 ;
            }
            
            ShoulderHinge.motor = ShoulderMotor;
        }
        if (_inputManager.Get_Action_Release(playerNumber))
        {
            ShoulderMotor.motorSpeed = 0f;
            ShoulderHinge.motor = ShoulderMotor;
            pickup.Release();
            currentRock.GetComponent<Rigidbody2D>().mass = 10;
            currentRock.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Kill"))
        {
            Debug.Log("Kill");
            if (playerNumber ==Input_Manager.PlayerNumber.P1)
            {
                 SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P2);
            }
            else
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P1);
            }
           
        }
    }
}