using UnityEngine;

// Handles bullet damage and collision behavior
public class BulletAttribute : MonoBehaviour
{
    public float damage = 100f; // amount of damage this bullet deals

    private SpriteRenderer sr; // reference to the sprite renderer (used for color change)

    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); // get the sprite renderer component from this object
    }

    void Update()
    {
        // if the sprite renderer exists, change the color over time
        if (sr != null)
        {
            // smoothly changes between red and yellow using a ping pong effect
            sr.color = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time * 5f, 1f));
        }
    }

    // called when the bullet enters a trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // try to get the Enemy script from the object that was hit
        Enemy enemy = other.GetComponent<Enemy>();

        // if not found, check the parent object (in case collider is on a child)
        if (enemy == null)
            enemy = other.GetComponentInParent<Enemy>();

        // if an enemy was found, deal damage and destroy the bullet
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // apply damage to enemy
            Destroy(gameObject); // destroy the bullet
            return; // stop checking further
        }

        // try to get the BossController script from the object that was hit
        BossController boss = other.GetComponent<BossController>();

        // if not found, check the parent object
        if (boss == null)
            boss = other.GetComponentInParent<BossController>();

        // if a boss was found, deal damage and destroy the bullet
        if (boss != null)
        {
            boss.TakeDamage(damage); // apply damage to boss
            Destroy(gameObject); // destroy the bullet
            return; // stop checking further
        }
    }

    
}