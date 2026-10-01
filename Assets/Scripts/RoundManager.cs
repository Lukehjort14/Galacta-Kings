using UnityEngine;

// Manages game rounds, boss spawning, and enemy cleanup
public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    public int roundNumber = 1; // current round number
    public float baseBossHealth = 500f; // starting boss health
    public float healthIncrease = 200f; // extra health added each round
    public float baseBossSpeed = 3f; // Starting boss speed
    public float speedIncrease = 0.5f; // amount of speed added to boss each round

    [Header("Boss Spawn")]
    public GameObject bossPrefab; // boss prefab to spawn
    public Transform bossSpawnPoint; // location where boss spawns

    void Start()
    {
        Debug.Log("RoundManager started"); // log when manager starts

        // save the starting round into leaderboard data
        LeaderboardData.roundNumber = roundNumber;

        StartRound(); // begin first round
    }

    // starts a new round and spawns the boss
    void StartRound()
    {
        Debug.Log("StartRound called"); // log function call

        if (bossPrefab == null)
        {
            Debug.Log("Boss prefab is missing!"); // warn if prefab is not assigned
            return; // stop execution
        }

        if (bossSpawnPoint == null)
        {
            Debug.Log("Boss spawn point is missing!"); // warn if spawn point is not assigned
            return; // stop execution
        }

        Debug.Log("Starting Round " + roundNumber); // log current round

        // update leaderboard with the current round
        LeaderboardData.roundNumber = roundNumber;

        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity); // spawn boss

        if (boss == null)
        {
            Debug.Log("Boss did not spawn!"); // warn if spawn failed
            return; // stop execution
        }

        BossController bossScript = boss.GetComponent<BossController>(); // get boss script

        if (bossScript != null)
        {
            bossScript.health = baseBossHealth + ((roundNumber - 1) * healthIncrease); // set boss health based on round
            bossScript.moveSpeed = baseBossSpeed + ((roundNumber - 1) * speedIncrease); // set boss speed based on round

            Debug.Log("Boss health set to " + bossScript.health); // log new health
        }
        else
        {
            Debug.Log("BossController not found on spawned boss!"); // warn if script missing
        }
    }

    // called when boss is defeated to move to next round
    public void BossDefeated()
    {
        ClearEnemies(); // remove all remaining enemies

        roundNumber++; // increase round number

        // update leaderboard with the new round
        LeaderboardData.roundNumber = roundNumber;

        Debug.Log("Round is now " + roundNumber); // log new round

        Invoke("StartRound", 3f); // start next round after delay
    }

    // destroys all enemies currently in the scene
    void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // find all objects tagged as Enemy

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // destroy each enemy
        }
    }
}