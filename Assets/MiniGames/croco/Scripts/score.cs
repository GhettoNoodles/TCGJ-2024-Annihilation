using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class score : MonoBehaviour
{
    [SerializeField] private int OneScore = 0, TwoScore = 0;
    private Input_Manager.PlayerNumber winner;
    [SerializeField] private GameObject holdPanel;
    [SerializeField] private TextMeshProUGUI p1text;
    [SerializeField] private TextMeshProUGUI p2text;
    public int teeth = 7;
    [SerializeField] private float minGameTime;

    // Start is called before the first frame update
    void Start()
    {
        minGameTime += SceneBehaviour.Instance.GameTime;
        SceneBehaviour.Instance.currentGameTime = minGameTime;
        SceneBehaviour.Instance.GameLoaded();
    }
    public void AddOneToOne()
    {
        OneScore++;
        teeth--;
        p1text.text = OneScore.ToString();
        Debug.Log(OneScore);
    }
    
    public void AddOneToTwo()
    {
        TwoScore++;
        teeth--;
        p2text.text = TwoScore.ToString();
        Debug.Log(TwoScore);
    }
    

    private void Update()
    {
        if (Time.timeSinceLevelLoad>SceneBehaviour.Instance.currentGameTime)
        {
            if (OneScore>TwoScore)
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P1);
            }
            else if (TwoScore>OneScore)
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P2);
            }
            else
            {
                SceneBehaviour.Instance.EndGameSession((Input_Manager.PlayerNumber)Random.Range(0, 2));
            }
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
