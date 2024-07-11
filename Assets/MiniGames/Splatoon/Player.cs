using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    private Input_Manager _inputManager = Input_Manager.Instance;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

// Update is called once per frame
    void Update()
    {
        rb.velocity = _inputManager.Get_Stick(playerNumber)*speed;
    }
}