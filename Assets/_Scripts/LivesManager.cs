using UnityEngine;
using System.Collections.Generic;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    [Header("Ustawienia żyć")]
    public int maxLives = 3;
    private int currentLives;

    [Header("Prefab serca (asset)")]
    public GameObject heartPrefab;
    public Transform heartsContainer;
    public float spacing = 0.3f; // mniejszy odstęp

    private List<GameObject> heartsList = new List<GameObject>();

    [Header("Starter Banana")]
    public GameObject starterBananaPrefab;
    public Transform starterSpawnPoint;

    public bool IsGameOver { get; private set; }

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
        if (currentLives <= 0) return;

        currentLives--;
        UpdateHearts();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives;
        IsGameOver = false;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        // Usuń stare serca
        foreach (GameObject heart in heartsList)
        {
            Destroy(heart);
        }
        heartsList.Clear();

        // Wylicz przesunięcie dla wyrównania od prawej do lewej
        float totalWidth = (maxLives - 1) * spacing;

        // Dodaj nowe serca od prawej do lewej
        for (int i = 0; i < currentLives; i++)
        {
            Vector3 offset = new Vector3(totalWidth - i * spacing, 0, 0);
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer.position + offset, Quaternion.identity, heartsContainer);
            heartsList.Add(newHeart);
        }
    }


    void GameOver()
    {
        Debug.Log("GAME OVER!");
        IsGameOver = true;

        // Zatrzymaj wszystkie spawnery
        foreach (var spawner in FindObjectsOfType<FruitSpawner>())
        {
            spawner.StopSpawning();
        }

        // Wznów starter banana
        if (starterBananaPrefab != null && starterSpawnPoint != null)
        {
            Instantiate(starterBananaPrefab, starterSpawnPoint.position, starterSpawnPoint.rotation);
        }
    }
}
