using UnityEngine;
using UnityEngine.UI;

// Handles the player's health and death behavior
public class PlayerHealth : MonoBehaviour

{
    public float maxHealth = 200f;
    public float health = 200f; // starting health of the player

    public Image healthBarFill;
    public Image healthBarDamage;

    



    public GameObject gameOverScreen; // reference to the game over UI
    public float damageSmoothSpeed = 2f; // controls the delay

    private bool isDead = false; // prevents death logic from running multiple times

    void Update()
    {
        UpdateHealthBar();   
    }

    // called when the player takes damage
    public void TakeDamage(float damage)
    {
        health -= damage; // reduce health by damage amount
        Debug.Log("Player HP: " + health); // print current health to console

        // check if player is dead and hasn't already died
        if (health <= 0 && !isDead)
        {
            isDead = true; // mark player as dead

            LeaderboardData.AddScore();// checks if the score is higher than the last save high score

            gameOverScreen.SetActive(true); // show the game over screen

            Time.timeScale = 0f; // freeze the game

            Destroy(gameObject); // destroy the player object
        }
    }


    void UpdateHealthBar()
    {
        if (healthBarFill != null && healthBarDamage != null)
        {
            float percent = Mathf.Clamp01(health / maxHealth);

            healthBarFill.fillAmount = percent;

            healthBarDamage.fillAmount = Mathf.Lerp(
                healthBarDamage.fillAmount, percent, Time.deltaTime * damageSmoothSpeed
                );

        }
    }


    // unused method that destroys the player
    void Die()
    {
        Debug.Log("Player Destroyed!"); // log player destruction
        Destroy(gameObject); // destroy the player object
    }
}