using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subwayscroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 1;
    [SerializeField]
    private float exponentialfactor = 1.05f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //transform.position = new Vector2(transform.position.x, transform.position.y + scrollSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        scrollSpeed *= exponentialfactor;
    }
}
