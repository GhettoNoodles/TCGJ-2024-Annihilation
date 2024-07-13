using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public Input_Manager.PlayerNumber whoseBulletisIt;
    public shootyScore scorescript;
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
            if (this.GetComponent<Bullet>().whoseBulletisIt == Input_Manager.PlayerNumber.P1)
            {
                scorescript.incOne();
            }
            else if (this.GetComponent<Bullet>().whoseBulletisIt == Input_Manager.PlayerNumber.P2)
            {
                scorescript.incTwo();
            }
            
        }
        Destroy(gameObject);
    }
}

