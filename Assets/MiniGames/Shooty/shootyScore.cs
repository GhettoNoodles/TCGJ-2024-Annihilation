using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class shootyScore : MonoBehaviour
{
    [SerializeField] private GameObject holdPanel;
    [SerializeField] private TextMeshProUGUI p1text;
    [SerializeField] private TextMeshProUGUI p2text;
    public static shootyScore Instance { get; private set; }
    public int playerOne = 0, playerTwo = 0;[SerializeField] private float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        SceneBehaviour.Instance.currentGametimer = gameTime;
        playerOne = 0;
        playerTwo = 0;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= gameTime)
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
        p1text.text = playerOne.ToString();
        Debug.Log("ONE: " + playerOne);
    }

    public void incTwo()
    {
        playerTwo++;
        p2text.text = playerTwo.ToString();
        Debug.Log("TWO: " + playerTwo);
    }
    
    
}
