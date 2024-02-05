using UnityEngine;

public class BananaRespawn : MonoBehaviour
{
    public GameObject startingBananaPrefab;

    void Update()
    {
        // SprawdŸ, czy obiekt o tagu "StartingBanana" istnieje
        GameObject startingBanana = GameObject.FindGameObjectWithTag("StartingBanana");

        // Jeœli obiekt zosta³ zniszczony lub go nie ma
        if (startingBanana == null)
        {
            RespawnBanana();
        }
    }

    void RespawnBanana()
    {
        float randomZ = Random.Range(-16.0f, -14.0f); // Losowa koordynata Z od -16 do -14
        float randomY = 1.4f;
        float randomX = Random.Range(3.0f, 6.0f);

        // Stwórz nowy obiekt StartingBanana w losowych koordynatach
        GameObject newBanana = Instantiate(startingBananaPrefab, new Vector3(randomX, randomY, randomZ), Quaternion.identity);
    }
}
