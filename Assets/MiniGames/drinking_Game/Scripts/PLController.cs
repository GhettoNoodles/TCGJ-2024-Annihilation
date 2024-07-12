using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLController : MonoBehaviour
{
    [SerializeField]
    private Input_Manager _inputManager;
    [SerializeField]
    UI_Manager ui_Manager;
    [SerializeField]
    private SwayBehaviour sway;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    [SerializeField] private float speed, drinkspeed, TimerCountdown, TimerReset, DecreaseDrag;
    [SerializeField] private Transform BeerStartPos, SwayPos, PawStart;
    [SerializeField] private bool CanGrab = false, ThereIsBeer = true, TimerReseted = false;
    [SerializeField] private GameObject Beer, BeerPrefab, CamParent;
    [SerializeField]
    private int minX, maxX, Ypos;
    public int TotalBeersDrank;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        transform.position = PawStart.position;
        ui_Manager.Set_Score(playerNumber);
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
                DrinkBeer();
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrinkBeer();
        }
        
    }

    private void DrinkBeer()
    {
        Destroy(Beer);
        sway.DrankBeer();
        ThereIsBeer = false;
        rb.drag -= DecreaseDrag;
        TotalBeersDrank += 1;
        ui_Manager.Set_Score(playerNumber);
    }

    private void SpawwnBeer()
    {
        float checkX = CamParent.transform.position.x;
        float RandomX;
        if (checkX <= BeerStartPos.position.x)
        {
            float UpperLimit = BeerStartPos.position.x + maxX;
            RandomX = UnityEngine.Random.Range(CamParent.transform.position.x, UpperLimit);

        }

        else
        {
            float LowerLimit = BeerStartPos.position.x - maxX;
            RandomX = UnityEngine.Random.Range(LowerLimit, CamParent.transform.position.x);
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
