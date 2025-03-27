using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitScoreManager : MonoBehaviour
{
    public static FruitScoreManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }

    public int GetScore() => score;

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "Twój wynik: " + score.ToString();
    }
}
