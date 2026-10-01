using UnityEngine;

// Handles player shooting behavior
public class ShipShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // reference to the bullet prefab
    public float bulletSpeed = 15f; // speed the bullet travels

    public float fireRate = 0.25f; // time between shots
    private float nextFireTime = 0f;

    void Update()
    {
        // check for left mouse click input
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Shoot(); // call shoot function

            nextFireTime = Time.time + fireRate;
        }
    }

    // creates and launches a bullet
    void Shoot()
    {
        GameObject bullet = Instantiate( // spawn a new bullet at player position
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); // get the bullet's Rigidbody2D
        rb.linearVelocity = Vector2.up * bulletSpeed; // move bullet upward at set speed
    }
}