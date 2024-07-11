using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatoon_Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Color p1;
    [SerializeField] private Color p2;
    public Dominance currentDominance; 

    public enum Dominance
    {
        neutral, p1,p2
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDominance = Dominance.neutral;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Paint(Input_Manager.PlayerNumber player)
    {
        if (player == Input_Manager.PlayerNumber.P1)
        {
            _sprite.color = p1;
            currentDominance = Dominance.p1;
        }
        else
        {
            _sprite.color = p2;
            currentDominance = Dominance.p2;
        }
        
    }
}
