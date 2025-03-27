using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public Transform spawnPoint;
    public float launchForce = 5f;

    private Coroutine spawnRoutine;

    void Start()
    {
        StartCoroutine(WaitForStarterBananaToBeCut());
    }

    IEnumerator WaitForStarterBananaToBeCut()
    {
        while (!StarterBanana.wasDestroyed)
            yield return null;

        Debug.Log("Banana przecięty! Zaczynamy spawn.");
        spawnRoutine = StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (!LivesManager.Instance.IsGameOver)
        {
            yield return new WaitForSeconds(2f);

            GameObject fruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = fruit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.velocity = new Vector3(0f, 1f, 1f).normalized * launchForce;
        }
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);
    }
}
