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
    [SerializeField] private float speed;
    [SerializeField] private Transform startpos, Pawposition, ConsumePos, SpawnPos;
    [SerializeField] private bool CanGrab = false, CanMove = true;
    [SerializeField] private GameObject Beer, BeerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        transform.position = startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
       if (CanMove == true)
        {
            var velocity = _inputManager.Get_Stick(playerNumber);

            // rb.velocity = velocity.normalized * speed;
            // transform.rotation = (Quaternion.LookRotation(Vector3.forward, velocity));
            if (velocity.magnitude > 0.1f)
            {
                rb.AddForce(velocity * speed);
            }
        }
        


        if (_inputManager.Get_Action(playerNumber))
        {
            if (CanGrab == true)
            {
                Beer.transform.position = Pawposition.position;
                Beer.transform.parent = transform;
                sway.DrankBeer();
                CanMove = false;
            }
        }

        if (CanMove == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, ConsumePos.position, speed * Time.deltaTime);

            if (transform.position == ConsumePos.position)
            {
                CanMove = true;
                CanGrab = false;
                Destroy(Beer);
                Instantiate(BeerPrefab, SpawnPos.position, Quaternion.identity);
            }
        }
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


}
