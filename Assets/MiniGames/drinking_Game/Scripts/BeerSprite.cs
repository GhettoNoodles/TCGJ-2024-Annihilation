using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        int num = UnityEngine.Random.Range(0, sprites.Length);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
