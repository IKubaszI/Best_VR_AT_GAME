using UnityEngine;

public class BananaSplash : MonoBehaviour
{
    public GameObject splashEffectPrefab; // Prefab efektu rozprysku
    public AudioSource splashSound; // Dźwięk rozprysku
    public Material crossSectionMaterial; // Materiał na "wnętrze" banana

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) // Jeśli dotknie podłogi
        {
            Splash();
        }
    }

    void Splash()
    {
        // Efekt rozprysku
        if (splashEffectPrefab != null)
        {
            Instantiate(splashEffectPrefab, transform.position, Quaternion.identity);
        }

        // Dźwięk rozprysku
        if (splashSound != null)
        {
            AudioSource.PlayClipAtPoint(splashSound.clip, transform.position);
        }

        // Symulacja rozwalenia banana
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        BoxCollider collider = GetComponent<BoxCollider>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();

        if (mesh != null) mesh.enabled = false;
        if (collider != null) collider.enabled = false;
        if (meshCollider != null) meshCollider.enabled = false;

        Destroy(gameObject, 2); // Usuwa banana po 2 sekundach
    }
}
