using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;     // Prefab banana
    public Transform spawnPoint;       // Miejsce spawnu
    public float launchForce = 5f;     // Siła wyrzutu

    private bool canSpawn = false;

    void Start()
    {
        StartCoroutine(WaitForStartingBanana());
    }

    IEnumerator WaitForStartingBanana()
    {
        // Czekaj aż StartingBanana zniknie z hierarchii
        while (GameObject.FindGameObjectWithTag("StartingBanana") != null)
        {
            yield return null;
        }

        Debug.Log("StartingBanana zniszczony – zaczynamy spawn!");
        canSpawn = true;
        StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (canSpawn)
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
}
