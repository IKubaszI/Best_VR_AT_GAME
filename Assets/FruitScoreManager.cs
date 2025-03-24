using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitScoreManager : MonoBehaviour{
    public static FruitScoreManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;

    private int score = 0;

    void Awake()
    {
        // Singleton — tylko jedna instancja
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScoreDisplay();
    }

    public int GetScore()
    {
        return score;
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Twoj wynik: " + score.ToString();
        }
    }
}
