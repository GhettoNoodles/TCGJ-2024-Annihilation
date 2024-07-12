using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwayBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SwayPositions;
    [SerializeField]
    private Transform CamPos;
    [SerializeField]
    private float AlcoholLevel;
    [SerializeField]
    private int AmountBeersDrank, Range, MaxSpeed;
    [SerializeField]
    private float SwaySpeed, FormulaBase, TimerReset, TimerCountdown, radius;
    [SerializeField]
    private bool CanMove = true, numPicked = false;
    [SerializeField]
    private Camera PlCam;
    
    
    // Start is called before the first frame update
    void Start()
    {
       CamPos.position = SwayPositions[0].position;


    }

    // Update is called once per frame
    void Update()
    {





        if (CanMove == true)
        {
            Swaying(SwayPositions[Range]);
        }


        if (CamPos.position == SwayPositions[Range].position)
        {
            PickSwayPos();
        }

        //if (CamPos.position == SwayPositions[Range].position ||
        //    CanMove == false)
        //{

        //    CanMove = false;
        //    if (CanMove == false)
        //    {
        //        //HowerAroundPos(SwayPositions[Range]);
        //        if (numPicked == false)
        //        {

        //            TimerReset = UnityEngine.Random.Range(0.3f, 0.8f);
        //            TimerCountdown = TimerReset;
        //            numPicked = true;
        //        }

        //        TimerCountdown -= Time.deltaTime;
        //        if (TimerCountdown <= 0)
        //        {
        //            PickSwayPos();
        //        }

        //    }


        //}


        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    DrankBeer();
        //}
    }

    //private void HowerAroundPos(Transform transform)
    //{
    //    Debug.Log(transform);
    //    Vector2 Bullshit = transform.position;
    //    Vector2 fuck = Bullshit + (UnityEngine.Random.insideUnitCircle * radius);
    //    CamPos.position = Vector2.MoveTowards(CamPos.position, fuck, (SwaySpeed/10) * Time.deltaTime);
    //}

    private void Swaying(Transform SwayTowards)
    {
        CamPos.position = Vector2.MoveTowards(CamPos.position, SwayTowards.position, SwaySpeed * Time.deltaTime);

        
    }

    public void DrankBeer()
    {
        AmountBeersDrank += 1;
        //AlcoholLevel = Mathf.Pow(2,AmountBeersDrank);
        AlcoholLevel = AlcoholLevel * 2;

        //SwaySpeed = 1f + (AlcoholLevel / 50);
        float Power = (2*AmountBeersDrank + 1);
        SwaySpeed = Mathf.Pow(FormulaBase, Power);

        if (SwaySpeed > MaxSpeed)
        {
            SwaySpeed = MaxSpeed;
        }
    } 

    public void PickSwayPos()
    {
        int num = AmountBeersDrank +1;
        
        if (num > SwayPositions.Count)
        {
            num = SwayPositions.Count-1;
        }
        
        Range = UnityEngine.Random.Range(0, num+1);
        CanMove = true;
        numPicked = false;
    }
   
}
