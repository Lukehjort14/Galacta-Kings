using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void playAgain()
    {
        Time.timeScale = 1f; //unpauses the game

        //reset score and the round
        LeaderboardData.playerScore = 0;
        LeaderboardData.roundNumber = 1;

        //reload game scene
        SceneManager.LoadScene("GameScene");
    }

    public void quitGame()
    {
        Debug.Log("Quit button pressed");

        Application.Quit();
    }

    // Loads the Leaderboard scene
    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    // Loads the Title Screen scene
    public void LoadTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");


    }
}