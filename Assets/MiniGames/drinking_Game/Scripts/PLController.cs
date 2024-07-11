using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLController : MonoBehaviour
{
    [SerializeField]
    private Input_Manager _inputManager;
    [SerializeField]
    private SwayBehaviour sway;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private float speed, drinkspeed, TimerCountdown, TimerReset;
    [SerializeField] private Transform startpos, SwayPos;
    [SerializeField] private bool CanGrab = false, ThereIsBeer = true, TimerReseted = false;
    [SerializeField] private GameObject Beer, BeerPrefab, CamParent;
    [SerializeField]
    private int minX, maxX, Ypos;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        transform.position = startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
      
        
            var velocity = _inputManager.Get_Stick(playerNumber);

            // rb.velocity = velocity.normalized * speed;
            // transform.rotation = (Quaternion.LookRotation(Vector3.forward, velocity));
            if (velocity.magnitude > 0.1f)
            {
                rb.AddForce(velocity * speed);
            }
        
        


        if (_inputManager.Get_Action_Tap(playerNumber))
        {
            if (CanGrab == true)
            {
                Destroy(Beer);
                sway.DrankBeer();
                ThereIsBeer = false;
                rb.drag -= 0.5f;
            }
        }
        if (ThereIsBeer == false)
        {
            if (TimerReseted == false)
            {
                TimerCountdown = TimerReset;
                TimerReseted = true;
            }

            TimerCountdown -= Time.deltaTime;

            if (TimerCountdown <= 0)
            {

                SpawwnBeer();
                
                ThereIsBeer = true;
                TimerReseted = false;
            }
        }
        
        
    }

    private void SpawwnBeer()
    {
        float checkX = CamParent.transform.position.x;
        float RandomX;
        if (checkX <= startpos.position.x)
        {
            RandomX = UnityEngine.Random.Range(CamParent.transform.position.x, maxX);

        }

        else
        {
            RandomX = UnityEngine.Random.Range(minX, CamParent.transform.position.x);
        }

        Vector2 SpawnPos = new Vector2(RandomX, Ypos);
        GameObject SetLocation = Instantiate(BeerPrefab, SpawnPos, Quaternion.identity);
        SwayPos.position = SetLocation.transform.position;
        sway.PickSwayPos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Beer")
        {
            CanGrab = true;
            Beer = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Beer")
        {
            CanGrab = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Beer")
        {
            CanGrab = true;
            Beer = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Beer")
        {
            CanGrab = true;
            Beer = collision.gameObject;
        }
    }
}
