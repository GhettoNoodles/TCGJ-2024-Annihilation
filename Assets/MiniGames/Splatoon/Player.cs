using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    private Input_Manager _inputManager;
    [SerializeField] private Splatoongame game;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private float speed;
    [SerializeField] private Color playerColor;
    [SerializeField] private float dashCooldownMax=2f;
    [SerializeField] private float dashCooldown = 0f;
    [SerializeField] private float dashMaxDistance =2f;
    private bool dashing = false;
    private bool stopped = true;
    private Vector2 startpos;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
    }

// Update is called once per frame
    void Update()
    {
        
       
        var velocity =_inputManager.Get_Stick(playerNumber);
        if(stopped){    rb.velocity = velocity.normalized * speed;}
    
        if (velocity!= Vector2.zero)
        {
            transform.rotation = (Quaternion.LookRotation(Vector3.forward,velocity));
        }
        if (_inputManager.Get_Action(playerNumber))
        {
            if (!dashing)
            {
                dashing = true;
                stopped = false;
                startpos = rb.position;
                rb.AddForce(transform.up*30,ForceMode2D.Impulse);
            }
            
        }
        if(dashing&&!stopped)
        {
            if (Vector2.Distance(startpos,rb.position)>=dashMaxDistance)
            {
                stopped = true;
                rb.velocity=Vector2.zero;
            }
        }
        if (dashing)
        {
            dashCooldown += Time.deltaTime;
            if (dashCooldown>=dashCooldownMax)
            {
                dashCooldown = 0;
                dashing = false;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Splatoon_Tile"))
        {
            
            other.GetComponent<Splatoon_Tile>().Paint(playerNumber);
            game.UpdateScore();
        }
    }
}