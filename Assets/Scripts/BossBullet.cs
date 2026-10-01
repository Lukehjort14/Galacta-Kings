
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    // How much damage the bullet does
    public float damage = 10f;

    // How long before the bullet deletes itself
    public float lifeTime = 5f;

    public Color green = new Color(0.2f, 1f, 0.2f);
    public Color yellow = new Color(1f, 0.9f, 0.2f);
    public float colorSpeed = 5f; 

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Destroy the bullet after a few seconds
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if(sr != null)
        {
            float t = Mathf.PingPong(Time.time * colorSpeed, 1f);
            sr.color = Color.Lerp(green, yellow, t); 
        }     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet hit the player
        if (other.CompareTag("Player"))
        {
            // Try to find the PlayerHealth script
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            // If the script exists, damage the player
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
