using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class score : MonoBehaviour
{
    [SerializeField] private int OneScore = 0, TwoScore = 0;

    public void AddOneToOne()
    {
        OneScore++;
        Debug.Log(OneScore);
    }
    
    public void AddOneToTwo()
    {
        TwoScore++;
        Debug.Log(TwoScore);
    }
}
