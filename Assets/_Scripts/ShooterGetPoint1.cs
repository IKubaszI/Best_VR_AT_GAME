using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShooterGetPoint1 : MonoBehaviour
{
    public int score = 0;
    public TMP_Text coins;

    void Start()
    {
        coins = GetComponent<TMP_Text>();
        UpdatePointsText();
    }

    public void resetscore()
    {
        score = 0;
        UpdatePointsText();
        
    }


    public void AddPoints(int amount)
    {
            score += amount;
            UpdatePointsText();
    }

    void UpdatePointsText()
    {
        coins.text = "Twoj wynik: " + score.ToString();
    }
}
