using UnityEngine;


public class BossController : MonoBehaviour
{
    [Header("Boss Stats")]
    public float moveSpeed = 3f;   // How fast the boss moves left and right
    public float health = 500f;    // Current health of the boss
    public int bossPointWorth = 100; // How much the boss is worth

    private int direction = 1;     // Used to control movement direction (1 = right, -1 = left)

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;   // Enemy that the boss will spawn
    public float spawnInterval = 3f; // Time between enemy spawns
    private float spawnTimer;        // Keeps track of time until next spawn

    [Header("Boss Shooting")]
    public GameObject bulletPrefab; // Bullet that the boss fires
    public Transform firePoint;     // Position where bullets will appear
    public float bulletSpeed = 6f;  // Speed of the bullet
    public float fireRate = 2f;     // Time between boss shots

    private float fireTimer;        // Timer used to control when the boss shoots

    public float totalDamageTaken = 0f; // Tracks all damage the boss has taken

    public GameObject explosionObject; // Explosion effect shown when boss dies

    void Start()
    {
        Transform exp = transform.Find("Explosion");

        if (exp != null)
        {
            explosionObject = exp.gameObject;
        }
        else
        {
            Debug.LogError("Explosion child not found!");
        }
    }

    void Update()
    {
        // Every frame we run these behaviors
        Move();            // Boss moves back and forth
        HandleSpawning();  // Boss spawns enemies
        HandleShooting();  // Boss shoots at the player
    }

    void Move()
    {
        // Move the boss left or right depending on the direction value
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        // If the boss reaches the right edge of the screen, reverse direction
        if (transform.position.x > 6f)
        {
            direction = -1;
        }
        // If the boss reaches the left edge of the screen, reverse direction
        else if (transform.position.x < -6f)
        {
            direction = 1;
        }
    }

    void HandleSpawning()
    {
        // Increase the spawn timer every frame
        spawnTimer += Time.deltaTime;

        // Once enough time has passed, spawn a new enemy
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // reset the timer
        }
    }

    void SpawnEnemy()
    {
        // If no enemy prefab was assigned, stop the function
        if (enemyPrefab == null) return;

        // Spawn the enemy slightly below the boss
        Vector3 spawnPos = transform.position + new Vector3(0f, -1.5f, 0f);

        // Create the enemy in the scene
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    void HandleShooting()
    {
        // Increase the shooting timer every frame
        fireTimer += Time.deltaTime;

        // When the timer reaches the fire rate, shoot a bullet
        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f; // reset shooting timer
        }
    }

    void Shoot()
    {
        // Make sure the bullet prefab and fire point exist
        if (bulletPrefab == null || firePoint == null) return;

        // Create the bullet at the fire point location
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Find the player object in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If the player exists, aim the bullet toward them
        if (player != null)
        {
            // Calculate direction from boss to player
            Vector2 direction = (player.transform.position - firePoint.position).normalized;

            // Give the bullet velocity so it travels toward the player
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // If the bullet has a Rigidbody2D, apply movement to it
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        // Stop extra damage from being applied after the boss is already dead
        if (health <= 0) return; // prevents extra hits after death

        // Reduce boss health when hit by the player's bullets
        health -= damage;
        totalDamageTaken += damage;

        // If health reaches zero, the boss dies
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Congratulations the Boss has been Defeated!");

        LeaderboardData.playerScore += bossPointWorth;

        RoundManager roundManager = FindFirstObjectByType<RoundManager>();

        if (roundManager != null)
        {
            roundManager.BossDefeated();
        }

        // Turn on the explosion
        if (explosionObject != null)
        {
            explosionObject.SetActive(true);
            explosionObject.transform.parent = null;
        }

        Destroy(gameObject);
    }
}