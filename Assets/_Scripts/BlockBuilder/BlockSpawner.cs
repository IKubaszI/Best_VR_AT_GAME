using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject blockPrefab;
    public Transform spawnPoint;
    public float spawnForce = 2f;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Metoda dla Twojego ButtonVR
    public void SpawnBlock()
    {
        if (blockPrefab != null && spawnPoint != null)
        {
            GameObject newBlock = Instantiate(blockPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Dodaj lekką losową siłę
            Rigidbody rb = newBlock.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomForce = new Vector3(
                    Random.Range(-spawnForce, spawnForce), 
                    0, 
                    Random.Range(-spawnForce, spawnForce)
                );
                rb.AddForce(randomForce, ForceMode.Impulse);
            }
            
            // Odtwórz dźwięk
            if (audioSource != null)
            {
                audioSource.Play();
            }
            
            Debug.Log("Block spawned!");
        }
    }
}
