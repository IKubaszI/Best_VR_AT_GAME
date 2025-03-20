using UnityEngine;

public class BananaGroundHandler : MonoBehaviour
{
    public GameObject splashEffectPrefab; // Prefab efektu rozprysku
    private bool hasSplashed = false; // Flaga zapobiegająca ponownemu uruchomieniu

    void OnTriggerEnter(Collider other)
    {
        if (!hasSplashed && other.CompareTag("Ground"))
        {
            HandleBananaHit();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasSplashed && collision.gameObject.CompareTag("Ground"))
        {
            HandleBananaHit();
        }
    }

    void HandleBananaHit()
    {
        hasSplashed = true;
        Debug.Log("Banana uderzył o ziemię!");

        // Efekt rozprysku
        if (splashEffectPrefab != null)
        {
            GameObject splashEffect = Instantiate(splashEffectPrefab, transform.position, Quaternion.identity);
            AudioSource splashAudio = splashEffect.GetComponent<AudioSource>();

            if (splashAudio != null)
            {
                splashAudio.Play();
            }

            Destroy(splashEffect, 2f); // Usuń efekt po 2 sekundach
        }

        // Wyłączenie renderowania i kolizji banana
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();

        if (mesh != null) mesh.enabled = false;
        if (collider != null) collider.enabled = false;

        Destroy(gameObject, 2); // Usuń banana po 2 sekundach
    }
}
