using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InBetweenMan : MonoBehaviour
{
    [SerializeField] private Image p1ready;
    [SerializeField] private Image p2ready;
    [SerializeField] private TextMeshProUGUI timertxt;
    private float timer = 0;
    private bool ready;

    [SerializeField] private GameObject nextPanel;
    [SerializeField] private GameObject readyPanel;[SerializeField] private GameObject laserP1;
    [SerializeField] private GameObject laserP2;
    private bool nextGame = false;
    // Start is called before the first frame update
    void Start()
    {
        nextPanel.SetActive(true);
        readyPanel.SetActive(false);
        if (SceneBehaviour.Instance.recentWinner ==1)
        {
            laserP1.SetActive(true);
        }
        else if (SceneBehaviour.Instance.recentWinner ==2)
        {
            laserP2.SetActive(true);
        }
        else
        {
            laserP1.SetActive(false);
            laserP2.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            laserP1.SetActive(false);
            laserP2.SetActive(false);
            nextPanel.SetActive(false);
            readyPanel.SetActive(true);
            nextGame = true;
        }
        if (nextGame)
        {
            p1ready.color = Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1)
                ? new Color(1, 0.5f, 0.5f, 1)
                : Color.white;

            p2ready.color = Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)
                ? new Color(0.5f, 0.5f, 1, 1)
                : Color.white;
            ready = (Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
                     Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2));
            if (ready)
            {
                timer -= Time.deltaTime;
                timertxt.text = timer.ToString("F0");
            }
            else
            {
                timertxt.text = "";
                timer = 3.49f;
            }

            if (timer <= 0.51f)
            {
                SceneBehaviour.Instance.NextGame();
            }
        }
    }
}
