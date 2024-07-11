using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Input_Manager _inputManager;

    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    public bool isStunned = false;
    public GameObject tooth;

    void Start()
    {
        _inputManager = Input_Manager.Instance;
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            var velocity = _inputManager.Get_Stick(playerNumber);
            rb.velocity = velocity.normalized * speed;
        }
    }

    public void StunPlayer(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == tooth.GetComponent<CircleCollider2D>());
        {
            Debug.Log("Tooth picked up!");
            if (_inputManager.Get_Action(playerNumber))
            {
                Debug.Log("Action up!");
            }
            
            // Perform additional actions as needed (e.g., increase score, play sound)
            
        }
    }
    
}

