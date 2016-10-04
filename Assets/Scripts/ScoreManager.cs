using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public float incrementDelay = 0.5f;

    public void IncrementScore()
    {
        score++;
        UpdateScore();
    }

    public void IncrementScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public void UpdateScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        UIManager.instance.UpdateScore(score, highScore);
    }

    public void PersistHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
        print("Set High Score: " + highScore);
    }

    public void StartIncrement()
    {
        InvokeRepeating("IncrementScore", incrementDelay, incrementDelay);
    }

    public void StopIncrement()
    {
        CancelInvoke("IncrementScore");
        PersistHighScore();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        UpdateScore();
    }

    private void Start()
    {
        LoadHighScore();
    }
}