using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootyScore : MonoBehaviour
{
    [SerializeField] private GameObject holdPanel;
    public int playerOne = 0, playerTwo = 0;
    // Start is called before the first frame update
    void Start()
    {
        SceneBehaviour.Instance.GameLoaded();
        playerOne = 0;
        playerTwo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= SceneBehaviour.Instance.GameTime)
        {
            Input_Manager.PlayerNumber winner;
            if (playerOne > playerTwo)
            {
                winner = Input_Manager.PlayerNumber.P1;
            }
            else if (playerOne < playerTwo)
            {
                winner = Input_Manager.PlayerNumber.P2;
            }
            else
            {
                winner = (Input_Manager.PlayerNumber)Random.Range(0, 2);
            }

            SceneBehaviour.Instance.EndGameSession(winner);
        }

        if (!(Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
              Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)))
        {
            holdPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            holdPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    public void incOne()
    {
        playerOne++;
        Debug.Log("ONE: " + playerOne);
    }

    public void incTwo()
    {
        playerTwo++;
        Debug.Log("TWO: " + playerTwo);
    }
    
    
}
