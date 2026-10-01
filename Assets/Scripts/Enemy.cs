using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 50f;        // enemy health amount
    public float moveSpeed = 2f;      // speed moving downward
    public float waveSpeed = 3f;      // speed of zig-zag motion
    public float waveAmount = 2f;     // width of zig-zag movement
    public int pointWorth = 10;    // points given when enemy is destroyed

    private float startX; // starting X position (used for wave movement)
    private bool isDead = false; //stops score form being added more than once

    void Start()
    {
        // Store the starting X position so the wave motion is centered
        startX = transform.position.x;
    }

    void Update()
    {
        // Call movement every frame
        Move();
    }

    void Move()
    {
        // Move downward over time
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        // Calculate zig-zag (sine wave) horizontal movement
        float newX = startX + Mathf.Sin(Time.time * waveSpeed) * waveAmount;

        // Keep enemy inside screen bounds
        newX = Mathf.Clamp(newX, -13f, 13f);

        // Apply new position (X changes, Y continues downward)
        transform.position = new Vector3(newX, transform.position.y, 0f);

    }

    // Called when enemy takes damage
    public void TakeDamage(float damage)
    {
        //stop extra hits after enemy is already dying
        if (isDead) return;

        // Reduce health
        health -= damage;

        // If health reaches zero, destroy enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Handles enemy death
    void Die()
    {

        if (isDead) return ; 
        isDead = true ; 
    

        // Adds the worth of enemy to players score
        LeaderboardData.playerScore += pointWorth;

        // Destroy the enemy object
        Destroy(gameObject);


    }

    // Detect collision with other objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object hit has a PlayerHealth component
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            // Damage the player
            player.TakeDamage(20f); // damage amount

            // Destroy enemy on impact
            Destroy(gameObject);
        }
    }
}