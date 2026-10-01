using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardData : MonoBehaviour
{
    public static string playerName;
    public static int playerScore;
    public static int roundNumber;

    // Stores the top 5 leaderboard entries
    public static List<ScoreEntry> topScores = new List<ScoreEntry>();

    public static void AddScore()
    {
        // Add the current player's score
        topScores.Add(new ScoreEntry(playerName, playerScore, roundNumber));

        // Sort highest score first
        topScores = topScores
            .OrderByDescending(score => score.score)
            .Take(5)
            .ToList();
    }
}


// Stores one leaderboard entry
public class ScoreEntry
{
    public string name;
    public int score;
    public int round;

    public ScoreEntry(string newName, int newScore, int newRound)
    {
        name = newName;
        score = newScore;
        round = newRound;
    }
}