using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatoon_Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Color p1;
    [SerializeField] private Color p2;
    [SerializeField] private Sprite[] scribbles;
    [SerializeField] private SpriteRenderer gui;
    public Dominance currentDominance; 

    public enum Dominance
    {
        neutral, p1,p2
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDominance = Dominance.neutral;
        gui = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paint(Input_Manager.PlayerNumber player)
    {
        if (player == Input_Manager.PlayerNumber.P1)
        {
            gui.color = p1;
            currentDominance = Dominance.p1;
            ChangeSprite();
        }
        else
        {
            gui.color = p2;
            currentDominance = Dominance.p2;
            ChangeSprite();
        }
        
    }

    public void ChangeSprite() => gui.sprite = scribbles[Random.Range(0, scribbles.Length)];
}
