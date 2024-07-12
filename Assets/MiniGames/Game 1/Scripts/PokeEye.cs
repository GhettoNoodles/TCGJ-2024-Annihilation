using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class PokeEye : MonoBehaviour
{
    public GameObject Mouth;
    public Sprite[] mouth_sprites; 
    public float[] mouth_changePoint;

    public Sprite[] eyeSprites;
    public float[] changePoint;

    public Sprite[] finger1_sprite;
    public Sprite[] finger2_sprite;

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

    [SerializeField] private TextMeshProUGUI timertxt;
    private void Start()
    {
        Finger1.transform.position = new Vector3(Finger1.transform.position.x, startPos.position.y);
        Finger2.transform.position = new Vector3(Finger2.transform.position.x, startPos.position.y);
    }
    private void Update()
    {
        timertxt.text = (SceneBehaviour.Instance.GameTime - Time.timeSinceLevelLoad).ToString("F0");
        if (Time.timeSinceLevelLoad>SceneBehaviour.Instance.GameTime)
        {
            if (score_p1>score_p2)
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P1);
            }
            else
            {
                SceneBehaviour.Instance.EndGameSession(Input_Manager.PlayerNumber.P2);
            }
           
        }
        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P1))
        {
            score_p1++;
            PokeEyeAction(Finger1, finger1_sprite);
        }
        else
        {
            Finger1.transform.position = new Vector3(Finger1.transform.position.x, startPos.position.y);
            Finger1.GetComponentInChildren<SpriteRenderer>().sprite = finger1_sprite[0];
        }


        if (Input_Manager.Instance.Get_Action_Tap(Input_Manager.PlayerNumber.P2))
        {
            score_p2++;
            PokeEyeAction(Finger2, finger2_sprite);
        }
        else
        {
            Finger2.transform.position = new Vector3(Finger2.transform.position.x, startPos.position.y);
            Finger2.GetComponentInChildren<SpriteRenderer>().sprite = finger2_sprite[0];
        }
        
        if (Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P1))
        {
            PokeEyeAction(Finger1, finger1_sprite);
        }
        if (Input_Manager.Instance.Get_Action(Input_Manager.PlayerNumber.P2))
        {
            PokeEyeAction(Finger2, finger2_sprite);
        }

        MoistEyes();
        ChangeMouth();
    }

    private void MoistEyes()
    {
        score_p1 -= Time.deltaTime * moistSpeed;
        score_p2 -= Time.deltaTime * moistSpeed;

        EyeSpriteCheck(Eye1, score_p1);
        EyeSpriteCheck(Eye2, score_p2);
    }

    private void PokeEyeAction(GameObject finger, Sprite[] finger_sprites)
    {
        finger.transform.position = new Vector3(finger.transform.position.x, endPos.position.y);
        finger.GetComponentInChildren<SpriteRenderer>().sprite = finger_sprites[1];
    }

    private void EyeSpriteCheck(GameObject eye, float score)
    {
        if (score <= changePoint[0]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[0];
        else if (score <= changePoint[1]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[1];
        else if (score <= changePoint[2]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[2];
        else if (score <= changePoint[3]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[3];
        else if (score <= changePoint[4]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[4];
        else if (score <= changePoint[5]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[5];
        else if (score <= changePoint[6]) eye.GetComponentInChildren<SpriteRenderer>().sprite = eyeSprites[6];
    }

    private void ChangeMouth()
    {
        float totalDamage = score_p1 + score_p2;

        if (totalDamage <= mouth_changePoint[0]) Mouth.GetComponent<SpriteRenderer>().sprite = mouth_sprites[0];
        else if (totalDamage <= mouth_changePoint[1]) Mouth.GetComponent<SpriteRenderer>().sprite = mouth_sprites[1];
        else if (totalDamage <= mouth_changePoint[2]) Mouth.GetComponent<SpriteRenderer>().sprite = mouth_sprites[2];
        else if (totalDamage <= mouth_changePoint[3]) Mouth.GetComponent<SpriteRenderer>().sprite = mouth_sprites[3];
    }
}
