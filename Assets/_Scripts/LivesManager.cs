using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;
    public int maxLives = 3;
    private int currentLives;

    [Header("UI")]
    public TMP_Text livesText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + currentLives;
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");
        // TODO: Zatrzymaj grę, wyświetl ekran, wyczyść scenę
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }
}
