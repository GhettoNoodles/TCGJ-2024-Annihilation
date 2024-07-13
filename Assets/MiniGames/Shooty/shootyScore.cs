using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootyScore : MonoBehaviour
{
    public int playerOne = 0, playerTwo = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerOne = 0;
        playerTwo = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void incOne()
    {
        playerOne++;
        Debug.Log("ONE: " + playerOne);
    }

    public void incTwo()
    {
        playerTwo++;
        Debug.Log("TWO: " + playerTwo);
    }
    
    
}
