using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    // This is an array of text objects in the UI 
    public TMP_Text[] scoreTexts;

    private void Start()
    {
        // Loop through each text slot in the leaderboard
        for (int i = 0; i < scoreTexts.Length; i++)
        {
            // Check if we actually have a score for this position
            if (i < LeaderboardData.topScores.Count)
            {
                // Get the score entry from the list
                ScoreEntry entry = LeaderboardData.topScores[i];

                // Set the text to show:
                // Rank (1, 2, 3...), player name, score, and round
                scoreTexts[i].text =
                    (i + 1) + ". " +              // rank number
                    entry.name + " - " +          // player name
                    entry.score + " Points - Round " + // score
                    entry.round;                 // round reached
            }
            else
            {
                // If there is no score for this slot, clear the text
                scoreTexts[i].text = "";
            }
        }
    }
}