using UnityEngine;
using static Input_Manager;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public Input_Manager.PlayerNumber whoseBulletisIt;
    public Sprite[] sprites;
    void Start()
        
    {
        if (whoseBulletisIt == Input_Manager.PlayerNumber.P1)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
        }
        Destroy(gameObject, lifeTime);

        transform.rotation = (Quaternion.LookRotation(Vector3.forward, Input_Manager.Instance.Get_Stick(whoseBulletisIt)));

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(collision.gameObject);
            if (this.GetComponent<Bullet>().whoseBulletisIt == Input_Manager.PlayerNumber.P1)
            {
                shootyScore.Instance.incOne();
            }
            else if (this.GetComponent<Bullet>().whoseBulletisIt == Input_Manager.PlayerNumber.P2)
            {
                shootyScore.Instance.incTwo();
            }
            
        }
        Destroy(gameObject);
    }
}

