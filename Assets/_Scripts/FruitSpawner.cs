using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab; // Przypisz prefab banana w Inspectorze
    public Transform spawnPoint; // Punkt spawnu

    public float launchForce = 5f; // Siła wyrzutu owocu

    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    IEnumerator SpawnFruits()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Czekaj 2 sekundy

            GameObject fruit = Instantiate(fruitPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = fruit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = new Vector3(0f, 1f, 1f).normalized * launchForce; // W przód i do góry

            }
        }
    }
}
