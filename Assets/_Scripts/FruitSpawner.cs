using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public Transform spawnPoint;
    public float launchForce = 5f;

    private bool hasStarted = false;
    private bool isSpawning = true; 

    void Start()
    {
        StartCoroutine(WaitForStarterBananaToBeCut());
    }

    IEnumerator WaitForStarterBananaToBeCut()
    {
        while (!StarterBanana.wasDestroyed)
        {
            yield return null;
        }

        Debug.Log("Banana przecięty! Zaczynamy spawn.");
        StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (isSpawning) // <- Uwaga na to!
        {
            yield return new WaitForSeconds(2f);

            GameObject fruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = fruit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = new Vector3(0f, 1f, 1f).normalized * launchForce;
            }
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}
