using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    [Header("UI")]
    public TMP_Text multiplierText;

    private int comboCount = 0;
    private int currentMultiplier = 1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void OnFruitSliced()
    {
        comboCount++;

        // Aktualizacja mnożnika co 4 trafione owoce
        if (comboCount >= 4 && comboCount < 8) currentMultiplier = 2;
        else if (comboCount >= 8 && comboCount < 12) currentMultiplier = 4;
        else if (comboCount >= 12 && comboCount < 16) currentMultiplier = 8;
        else if (comboCount >= 16) currentMultiplier = 10;

        UpdateUI();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        currentMultiplier = 1;
        UpdateUI();
    }

    public int GetCurrentMultiplier()
    {
        return currentMultiplier;
    }

    private void UpdateUI()
    {
        if (multiplierText != null)
        {
            multiplierText.text = "x" + currentMultiplier.ToString();
        }
    }
}
