using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeEye : MonoBehaviour
{
    public GameObject Eye1;
    public GameObject Eye2;

    public float score_p1;
    public float score_p2;

    private void Update()
    {
        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P1)) score_p1 += 1;
        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P2)) score_p2 += 1;
    }

    private void MoistEyes()
    {
        score_p1 -= Time.deltaTime;
        score_p2 -= Time.deltaTime;
    }

}
