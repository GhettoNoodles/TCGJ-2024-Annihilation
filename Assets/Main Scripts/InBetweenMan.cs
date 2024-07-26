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

    private bool nextGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P1)||Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P2))
        {
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
