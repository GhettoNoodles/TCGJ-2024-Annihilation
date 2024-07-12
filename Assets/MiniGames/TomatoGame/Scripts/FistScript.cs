using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FistScript : MonoBehaviour
{
    public static FistScript Instance { get; private set; }

    public int Player1_score;
    public int Player2_score;

    public GameObject fist_1;
    public GameObject fist_2;

    public float rel_height1;
    public float rel_height2;

    [SerializeField] private Transform maxHeight;
    [SerializeField] private Transform minHeight;

    public Rigidbody2D rb_1;
    public Rigidbody2D rb_2;

    public float speedMulti;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        rb_1 = fist_1.GetComponent<Rigidbody2D>();
        rb_2 = fist_2.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetRelHeight();
        Debug.Log(rb_2.velocity.y);

    }

    private void FixedUpdate()
    {
        rb_1.AddForce(new Vector2(0, rel_height1) * speedMulti, ForceMode2D.Impulse);
        rb_2.AddForce(new Vector2(0, rel_height2) * speedMulti, ForceMode2D.Impulse);
    }

    private void GetRelHeight()
    {
        rel_height1 = (Input_Manager.Instance.Get_Stick(Input_Manager.PlayerNumber.P1).y);
        rel_height2 = (Input_Manager.Instance.Get_Stick(Input_Manager.PlayerNumber.P2).y);
    }
}
