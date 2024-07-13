using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    private Input_Manager _inputManager;
    [SerializeField] private Input_Manager.PlayerNumber playerNumber;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 10f;
    public float shootRate = 0.5f;
    private float shootCooldown;
    public Bullet bulletscripty;

    [SerializeField] private Vector2 pointer;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = Input_Manager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the direction from the input manager
        pointer = _inputManager.Get_Stick(playerNumber);

        // Reduce the cooldown timer
        shootCooldown -= Time.deltaTime;

        // Check if the player wants to shoot and the cooldown has finished
        if (_inputManager.Get_Action(playerNumber) && shootCooldown <= 0)
        {
            Shoot();
            shootCooldown = shootRate;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the bulletSpawn position
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bulletscripty = bullet.GetComponent<Bullet>();
        bulletscripty.whoseBulletisIt = playerNumber;
        // Get the Rigidbody2D component of the bullet and set its velocity
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = pointer.normalized * bulletSpeed;
    }
}