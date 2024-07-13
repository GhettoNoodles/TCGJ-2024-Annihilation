using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class playerScript : MonoBehaviour
{
    
    private Input_Manager _inputManager;

    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private bool InPickUpRange, PickedUp;
    public bool isStunned = false;
    public GameObject pickingUptooth;
    public Vector2 ogPos;
    public tooth toothScript;
    private Vector2 minBoundary;
    private Vector2 maxBoundary;
    

    void Start()
    {
        _inputManager = Input_Manager.Instance;
        InPickUpRange = false;
        PickedUp = false;
        
        Camera cam = Camera.main;
        minBoundary = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        maxBoundary = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));
    }

    void FixedUpdate()
    {
        if (!isStunned)
        {
            var velocity = _inputManager.Get_Stick(playerNumber);
            rb.velocity = velocity.normalized * speed;

            // Clamp the player's position within the boundaries
            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(rb.position.x, minBoundary.x, maxBoundary.x),
                Mathf.Clamp(rb.position.y, minBoundary.y, maxBoundary.y)
            );
            rb.position = clampedPosition;
        }

        if (InPickUpRange)
        {
            PickUp();
        }
        
        NotpickedUp();
        
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
       if (other.CompareTag("tooth"))
       {
           InPickUpRange = true;
           pickingUptooth = other.gameObject;
           ogPos = pickingUptooth.transform.position;
           Debug.Log("can pick up");
       }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("tooth") && !toothScript.pickedUpbyPlayer)
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
            toothScript = pickingUptooth.GetComponent<tooth>();
            toothScript.pickedUpbyPlayer = true;
            pickingUptooth.transform.SetParent(this.transform);
            pickingUptooth.transform.localPosition = Vector3.zero;
            
        }
    }

    private void NotpickedUp()
    {
        if (!_inputManager.Get_Action(playerNumber))
        {
            //toothScript = pickingUptooth.GetComponent<tooth>();
            toothScript.pickedUpbyPlayer = false;
            //pickingUptooth.transform.parent = null;
        }
    }

    public void dropWhenStun()
    {
        toothScript.pickedUpbyPlayer = false;
        InPickUpRange = false;
    }
    
}

