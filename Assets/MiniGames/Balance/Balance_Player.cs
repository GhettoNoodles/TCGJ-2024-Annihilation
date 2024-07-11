using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Balance_Player : MonoBehaviour
{
    private Input_Manager _inputManager;
    [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Input_Manager.PlayerNumber playerNumber;
   [SerializeField] private GameObject rockObject;
   [SerializeField] private Rigidbody2D handrb;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = _inputManager.Get_Stick(playerNumber);
        rb.velocity = velocity.normalized * speed;
        if (_inputManager.Get_Action_Tap(playerNumber))
        {
            Instantiate(rockObject, handrb.position, quaternion.identity);
        }
    }
}
