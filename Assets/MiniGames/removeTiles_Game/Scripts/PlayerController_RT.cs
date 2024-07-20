using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_RT : MonoBehaviour
{
    private Input_Manager _inputManager;
    public Input_Manager.PlayerNumber playerNumber;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float Speed, IncreaseSpeed, AttackReset, AttackCooldown, MaxSpeed;
    [SerializeField]
    public Attack attack;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private bool CanAttack = false, RunTest = false;
    [SerializeField]
    private List<GameObject> TestAttack;
    public bool ScoreOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        ResetAttack();
    }

    // Update is called once per frame
    void Update()
    {

        var velocity = _inputManager.Get_Stick(playerNumber);
        float NewSpeed = Speed + IncreaseSpeed* Time.deltaTime;
        
        if (NewSpeed < MaxSpeed)
        {
            rb.velocity = velocity.normalized * NewSpeed;
        }
        
        else if (NewSpeed >= MaxSpeed)
        {
            rb.velocity = velocity.normalized * MaxSpeed;
        }
        

        

        if (_inputManager.Get_Action_Tap(playerNumber) &&
            CanAttack == true) 
        {
            Attack_OtherPlayer();
           
            ResetAttack();
        }

        if (AttackCooldown >= 0 &&
            CanAttack == false)
        {
            attack.gameObject.SetActive(false);
            AttackCooldown -= Time.deltaTime;
        }

        if (AttackCooldown < 0)
        {
            attack.gameObject.SetActive(true);
            CanAttack = true;
        }

       //if (RunTest == true)
       // {
       //     if (playerNumber == Input_Manager.PlayerNumber.P1)
       //     {
       //         for (int i = 0; i < TestAttack.Count; i++)
       //         {
       //             int num = TestAttack[i].GetComponent<Tile>().Tilenum;

       //         }
       //     }

       //     else if (playerNumber == Input_Manager.PlayerNumber.P2)
       //     {
       //         for (int i = 0; i < TestAttack.Count; i++)
       //         {
       //             int num = TestAttack[i].GetComponent<Tile>().Tilenum;

       //         }
       //     }
       // }
        
        


    }

    private void Attack_OtherPlayer()
    {
        List<GameObject> TheseTiles = attack.TilesInRange;


        if (playerNumber == Input_Manager.PlayerNumber.P1)
        {
            for (int i = 0; i < TheseTiles.Count; i++)
            {
                int num = TheseTiles[i].GetComponent<Tile>().Tilenum;
                gridManager.SetTileOnFire_PL1(num);
            }
        }

        else if (playerNumber == Input_Manager.PlayerNumber.P2)
        {
            for (int i = 0; i < TheseTiles.Count; i++)
            {
                int num = TheseTiles[i].GetComponent<Tile>().Tilenum;
                gridManager.SetTileOnFire_PL2(num);
            }
        }

        
    }

    public void ResetAttack()
    {
        CanAttack = false;
        attack.TilesInRange.Clear();
        AttackCooldown = AttackReset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Floor" &&
           (collision.GetComponent<Tile>().tileState == Tile.TileState.Burning ||
           collision.GetComponent<Tile>().tileState == Tile.TileState.Destroyed))
        {
            if (playerNumber == Input_Manager.PlayerNumber.P1 &&
                ScoreOnce == true)
            {
                ScoreOnce = false;
                gridManager.PL2_Scores();
                
            }

            else if(playerNumber == Input_Manager.PlayerNumber.P2 &&
                ScoreOnce == true)
            {
                ScoreOnce = false;
                gridManager.PL1_Scores();
                
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Floor" &&
           (collision.GetComponent<Tile>().tileState == Tile.TileState.Burning ||
           collision.GetComponent<Tile>().tileState == Tile.TileState.Destroyed))
        {
            if (playerNumber == Input_Manager.PlayerNumber.P1 &&
                ScoreOnce == true)
            {
                ScoreOnce = false;
                gridManager.PL2_Scores();
                
            }

            else if (playerNumber == Input_Manager.PlayerNumber.P2 &&
                ScoreOnce == true)
            {
                ScoreOnce = false;
                gridManager.PL1_Scores();
                
            }
        }
    }
}
