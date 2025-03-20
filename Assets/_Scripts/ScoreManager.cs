using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text pointsText; // Przypisz przez Inspector (np. obiekt UI > TextMeshPro Text)
    private int currentScore = 0;

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        pointsText.text = "Punkty: " + currentScore.ToString();
    }
}
