using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    [SerializeField] private int OneScore = 0, TwoScore = 0;
    private Input_Manager.PlayerNumber winner;
    public int teeth = 7;
    
    public void AddOneToOne()
    {
        OneScore++;
        teeth--;
        Debug.Log(OneScore);
    }
    
    public void AddOneToTwo()
    {
        TwoScore++;
        teeth--;
        Debug.Log(TwoScore);
    }

    private void Update()
    {
        if (teeth == 0)
        {
            if (OneScore > TwoScore)
            {
                winner = Input_Manager.PlayerNumber.P1;
            }
            else
            {
                winner = Input_Manager.PlayerNumber.P2;
            }
            SceneBehaviour.Instance.EndGameSession(winner);
        }
    }
}
