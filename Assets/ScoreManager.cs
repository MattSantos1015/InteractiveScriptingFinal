using UnityEngine;

public static class ScoreManager
{
    public static int totalPoints = 0;

    public static void AddPoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
    }
}