using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_RT : MonoBehaviour
{
    private Input_Manager _inputManager;
    [SerializeField] Input_Manager.PlayerNumber playerNumber;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float Speed, IncreaseSpeed, AttackReset, AttackCooldown, MaxSpeed;
    [SerializeField]
    public Attack attack;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private bool CanAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
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
        

        

        if ((_inputManager.Get_Action_Tap(playerNumber) ||
            Input.GetKeyDown(KeyCode.Space)) &&
            CanAttack == true) 
        {
            Attack_OtherPlayer();
            CanAttack = false;
            ResetAttack();
        }

        if (AttackCooldown >= 0 &&
            CanAttack == false)
        {
            AttackCooldown -= Time.deltaTime;
        }

        if (AttackCooldown < 0)
        {
            CanAttack = true;
        }

        
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
        attack.TilesInRange.Clear();
        AttackCooldown = AttackReset;
    }
}
