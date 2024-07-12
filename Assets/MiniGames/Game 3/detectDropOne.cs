using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectDropOne : MonoBehaviour
{
    public score scoreScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tooth"))
        {
            scoreScript.AddOneToOne();
            Destroy(other.gameObject);
        }
    }
}
