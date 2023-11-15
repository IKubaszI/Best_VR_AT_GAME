using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetPoint_Shooter : MonoBehaviour
{
    public int score_shooter = 0;
    private bool musicPlayed = false;

    void Start()
    {
        scoreOnHit = GetComponent<TMP_Text>();
        UpdatePointsText();
    }

    public void resetscore()
    {
        score_shooter = 0;
        UpdatePointsText();
        musicPlayed = false;
    }

    public void AddPointsShooter(int amount1)
    {
            score_shooter += amount1;
            UpdatePointsText();
    }

    void UpdatePointsText()
    {
        coins.text = "Twoj wynik: " + score_shooter.ToString();
    }
}
