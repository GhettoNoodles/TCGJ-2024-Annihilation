 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Score_PL1, Score_PL2;
    [SerializeField]
    private PLController PlayerOne, PlayerTwo;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Score(Input_Manager.PlayerNumber playerNumber)
    {
        if (playerNumber == Input_Manager.PlayerNumber.P1)
        {
            Score_PL1.text = PlayerOne.TotalBeersDrank.ToString();
        }

        else
        {
            Score_PL2.text = PlayerTwo.TotalBeersDrank.ToString();
        }
    }
}
