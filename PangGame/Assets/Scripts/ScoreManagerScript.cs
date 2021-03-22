using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    // Basic Score class to keep up with the higest score.
    public static ScoreManagerScript instance;
    public static int  score = 0 ,highScore = 0;
    public Text ScoreText;

    public static void AddScore()
    {
        score++;
    }
    public static void ResetScore()
    {
        score = 0;
    }

    public void UpdateScoreText()
    {
        ScoreText.text = highScore + "";
    }

    // Check if we want to change the highest score number.
    public static void UpdateNewScore()
    {
        if (highScore < score)
        {
            highScore = score;
        }
    }
}
