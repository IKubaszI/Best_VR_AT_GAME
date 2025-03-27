using UnityEngine;
using TMPro;
using System.Collections;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    public int maxLives = 3;
    private int currentLives;

    [Header("UI")]
    public TMP_Text livesText;

    [Header("Respawn Starter Banana")]
    public GameObject starterBananaPrefab;
    public Transform starterSpawnPoint;
    public float respawnDelay = 3f;

    public bool IsGameOver { get; private set; } // dodane tutaj!

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
        Debug.Log(" GAME OVER!");

        IsGameOver = true;

        // Zatrzymaj owocowe spawnery
        FruitSpawner[] fruitSpawners = FindObjectsOfType<FruitSpawner>();
        foreach (FruitSpawner spawner in fruitSpawners)
        {
            spawner.StopSpawning();
        }

        // Respawn starter banana
        StartCoroutine(RespawnStarterBananaAfterDelay());
    }

    IEnumerator RespawnStarterBananaAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (starterBananaPrefab != null && starterSpawnPoint != null)
        {
            Instantiate(starterBananaPrefab, starterSpawnPoint.position, starterSpawnPoint.rotation);
            ResetLives();
            FruitScoreManager.Instance?.ResetScore();
            IsGameOver = false;

            // Restart spawnery po respawnie StarterBanana
            FruitSpawner[] fruitSpawners = FindObjectsOfType<FruitSpawner>();
            foreach (FruitSpawner spawner in fruitSpawners)
            {
                spawner.RestartSpawning();
            }
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesUI();
        IsGameOver = false;
    }
}
