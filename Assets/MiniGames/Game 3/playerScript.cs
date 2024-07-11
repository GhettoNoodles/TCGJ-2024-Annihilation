using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Input_Manager _inputManager;

    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

// Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity =_inputManager.Get_Stick(playerNumber);
        rb.velocity = velocity.normalized * speed;
    }
    
}
