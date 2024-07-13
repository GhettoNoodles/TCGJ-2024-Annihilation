using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public Input_Manager.PlayerNumber whoseBulletisIt;

    void Start()
        
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}

