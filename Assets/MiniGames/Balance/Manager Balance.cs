using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ManagerBalance : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timertxt;
    [SerializeField] private TextMeshProUGUI p1text;
    [SerializeField] private TextMeshProUGUI p2text;
    [SerializeField] private GameObject holdPanel;
    private int p1Score;

    private int p2Score;

    public static ManagerBalance Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    } // Start is called before the first frame update
    void Start()
    {
    }

    public void IncreaseScore(Input_Manager.PlayerNumber loser)
    {
        if (loser == Input_Manager.PlayerNumber.P1)
        {
            p2Score++;
            p2text.text = p2Score.ToString();
        }
        else
        {
            p1Score++;
            p1text.text = p1Score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timertxt.text = (SceneBehaviour.Instance.GameTime - Time.timeSinceLevelLoad).ToString("F0");
        if (Time.timeSinceLevelLoad > SceneBehaviour.Instance.GameTime)
        {
            if (p1Score > p2Score)
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P1);
            }
            else
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P2);
            }
        }

        if (!(Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
              Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)))
        {
            Time.timeScale = 0f;
            holdPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            holdPanel.SetActive(false);
        }
    }
}