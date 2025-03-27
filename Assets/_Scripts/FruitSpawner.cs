using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public Transform spawnPoint;
    public float launchForce = 5f;

    private Coroutine spawnCoroutine;

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (true)
        {
            // Jeżeli istnieje StartingBanana, NIE SPAWNUJ owoców
            if (GameObject.FindGameObjectWithTag("StartingBanana") == null &&
                LivesManager.Instance != null &&
                !LivesManager.Instance.IsGameOver)
            {
                GameObject fruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = fruit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.velocity = new Vector3(0f, 1f, 1f).normalized * launchForce;
                }
            }
            
            yield return new WaitForSeconds(2f);
        }
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
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnFruits());
    }
}
