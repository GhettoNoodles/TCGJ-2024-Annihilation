using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PokeEye : MonoBehaviour
{
    public Sprite[] eyeSprites;
    public float[] changePoint;

    public GameObject Finger1;
    public GameObject Finger2;

    public GameObject Eye1;
    public GameObject Eye2;

    public Transform startPos;
    public Transform endPos;

    public float score_p1;
    public float score_p2;

    [Space(10)]
    public float moistSpeed;

    private void Start()
    {
        Finger1.transform.position = new Vector3(Finger1.transform.position.x, startPos.position.y);
        Finger2.transform.position = new Vector3(Finger2.transform.position.x, startPos.position.y);
    }
    private void Update()
    {
        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P1))
        {
            score_p1++;
            PokeEyeAction(Finger1);
        }
        else Finger1.transform.position = new Vector3(Finger1.transform.position.x, startPos.position.y);


        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P2))
        {
            score_p2++;
            PokeEyeAction(Finger2);

        }
        else Finger2.transform.position = new Vector3(Finger2.transform.position.x, startPos.position.y);
        
        if (Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P1))
        {
            PokeEyeAction(Finger1);
        }
        if (Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P2))
        {
            PokeEyeAction(Finger2);
        }

        MoistEyes();
    }

    private void MoistEyes()
    {
        score_p1 -= Time.deltaTime * moistSpeed;
        score_p2 -= Time.deltaTime * moistSpeed;

        EyeSpriteCheck(Eye1, score_p1);
        EyeSpriteCheck(Eye2, score_p2);
    }

    private void PokeEyeAction(GameObject finger)
    {
        finger.transform.position = new Vector3(finger.transform.position.x, endPos.position.y);
    }

    private void EyeSpriteCheck(GameObject eye, float score)
    {
        if (score <= changePoint[0]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[0];
        else if (score <= changePoint[1]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[1];
        else if (score <= changePoint[2]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[2];
        else if (score <= changePoint[3]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[3];
        else if (score <= changePoint[4]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[4];
    }
}
