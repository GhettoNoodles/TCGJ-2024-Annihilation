using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timertxt1;
    [SerializeField] private TextMeshProUGUI timertxt2;

    [SerializeField] private GameObject holdPanel;
    [SerializeField] private PLController p1;
    [SerializeField] private PLController p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timertxt1.text = (SceneBehaviour.Instance.GameTime - Time.timeSinceLevelLoad).ToString("F0");
        timertxt2.text = timertxt1.text;
        if (Time.timeSinceLevelLoad>SceneBehaviour.Instance.GameTime)
        {
            if (p1.TotalBeersDrank>p2.TotalBeersDrank)
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P1);
            }
            else if (p1.TotalBeersDrank<p2.TotalBeersDrank)
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
        
    }
}
