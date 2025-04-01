using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("Spawnery owoców")]
    public Transform[] spawnPoints;

    [Header("Prefaby owoców")]
    public GameObject[] fruitPrefabs;

    [Header("Prefab bomby")]
    public GameObject bombPrefab;

    [Header("Parametry wystrzału")]
    public float launchForce = 7f;
    public float arcHeightBoost = 1.5f;
    public float minDelay = 0.8f;
    public float maxDelay = 2f;

    [Header("Szansa na bombę")]
    [Range(0f, 1f)]
    public float bombChance = 0.2f;

    [Header("Cel rzutu (np. gracz/podest)")]
    public Transform targetPoint;

    [Header("Animatorzy małp dla każdego spawnera")]
    public Animator[] monkeyAnimators; // Małpy odpowiadające spawnerom

    private Coroutine spawnRoutine;

    void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }

    public void RestartSpawning()
    {
        StopSpawning();
        StartSpawning();
    }

    IEnumerator SpawnLoop()
    {
        yield return new WaitUntil(() =>
            GameObject.FindGameObjectWithTag("StartingBanana") == null &&
            LivesManager.Instance != null &&
            !LivesManager.Instance.IsGameOver
        );

        while (true)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[index];
            Animator monkeyAnimator = (monkeyAnimators.Length > index) ? monkeyAnimators[index] : null;

            GameObject prefabToSpawn;

            // Losuj bombę lub owoc
            if (Random.value < bombChance)
            {
                prefabToSpawn = bombPrefab;
            }
            else
            {
                prefabToSpawn = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            }

            if (prefabToSpawn != null && targetPoint != null)
            {
                GameObject obj = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = obj.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 direction = (targetPoint.position - spawnPoint.position).normalized;
                    direction.y += arcHeightBoost;
                    direction.Normalize();

                    rb.velocity = direction * launchForce;
                }

                // Wywołaj animację małpy
                if (monkeyAnimator != null)
                {
                    monkeyAnimator.SetTrigger("Throw"); // Trigger w animatorze
                }
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}
