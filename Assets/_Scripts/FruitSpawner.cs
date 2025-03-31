using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("Prefaby owoców i bomb")]
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;

    [Header("Wspólne spawnery")]
    public Transform[] spawnPoints;

    [Header("Opcje spawnowania")]
    public float launchForce = 5f;
    public float spawnInterval = 2f;
    [Range(0f, 1f)] public float bombChance = 0.2f; // 20% szans na bombę

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (CanSpawn())
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject prefabToSpawn;
                if (Random.value < bombChance && bombPrefab != null)
                {
                    prefabToSpawn = bombPrefab;
                }
                else
                {
                    prefabToSpawn = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
                }

                GameObject obj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = obj.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = new Vector3(0f, 1f, 1f).normalized * launchForce;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    bool CanSpawn()
    {
        return GameObject.FindGameObjectWithTag("StartingBanana") == null &&
               LivesManager.Instance != null &&
               !LivesManager.Instance.IsGameOver;
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public void RestartSpawning()
    {
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }
}
