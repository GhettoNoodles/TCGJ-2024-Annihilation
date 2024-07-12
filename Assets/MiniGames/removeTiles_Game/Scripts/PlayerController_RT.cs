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
    private float Speed, IncreaseSpeed, AttackReset, AttackCooldown;
    [SerializeField]
    private Attack attack;
    [SerializeField]
    private GridManager gridManager;
    [SerializeField]
    private bool CanAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
        attack = transform.GetChild(0).gameObject.GetComponent<Attack>();
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed += IncreaseSpeed * Time.deltaTime;
        
        
        var velocity = _inputManager.Get_Stick(playerNumber);
        rb.velocity = velocity.normalized * Speed;

        if (_inputManager.Get_Action_Tap(playerNumber) &&
            CanAttack == true) 
        {
            Attack_OtherPlayer();
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
                gridManager.DestroyTile_PL1(num);
            }
        }

        else if (playerNumber == Input_Manager.PlayerNumber.P2)
        {
            for (int i = 0; i < TheseTiles.Count; i++)
            {
                int num = TheseTiles[i].GetComponent<Tile>().Tilenum;
                gridManager.DestroyTile_PL2(num);
            }
        }
    }
}
