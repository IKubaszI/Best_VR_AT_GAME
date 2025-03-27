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
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ResetLives();
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
        Debug.Log("🎮 GAME OVER!");

        // Zatrzymaj wszystkie spawner-y owoców
        FruitSpawner[] fruitSpawners = FindObjectsOfType<FruitSpawner>();
        foreach (FruitSpawner spawner in fruitSpawners)
        {
            spawner.StopSpawning();
        }

    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }
}
