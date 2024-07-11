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
    [SerializeField] private bool InPickUpRange;
    public bool isStunned = false;
    public GameObject tooth, pickingUptooth;

    void Start()
    {
        _inputManager = Input_Manager.Instance;
        InPickUpRange = false;
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            var velocity = _inputManager.Get_Stick(playerNumber);
            rb.velocity = velocity.normalized * speed;
        }

        if (InPickUpRange)
        {
            PickUp();
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
       if (other == tooth.GetComponent<CircleCollider2D>())
       {
           InPickUpRange = true;
           pickingUptooth = other.gameObject;
           Debug.Log("can pick up");
       }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == tooth.GetComponent<CircleCollider2D>())
        {
            InPickUpRange = false;
            pickingUptooth = null;
            Debug.Log("no more");
        }
    }
    private void PickUp()
    {
        
        if (pickingUptooth != null && _inputManager.Get_Action(playerNumber))
        {
            Debug.Log("Picking up tooth!");
            pickingUptooth.transform.SetParent(this.transform);
            pickingUptooth.transform.localPosition = Vector3.zero; 
        }
    }
    
}

