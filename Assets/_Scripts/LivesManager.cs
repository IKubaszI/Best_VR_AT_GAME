using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    public int maxLives = 3;
    private int currentLives;

    [Header("UI")]
    public TMP_Text livesText; // Upewnij się, że przypisany w Inspectorze!

    public bool IsGameOver => currentLives <= 0;

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

    public void ResetLives()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = "Życia: " + currentLives.ToString();
    }

    void GameOver()
    {
        Debug.Log("GAME OVER!");

        // Wysłanie sygnału do wszystkich spawnerów
        FruitSpawner[] spawners = FindObjectsOfType<FruitSpawner>();
        foreach (var spawner in spawners)
        {
            spawner.StopSpawning();
        }

        // TODO: Dodać panel końca gry itd.
    }
}
