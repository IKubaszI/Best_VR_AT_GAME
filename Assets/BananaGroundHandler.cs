using UnityEngine;

public class BananaGroundHandler : MonoBehaviour
{
    public GameObject splashEffectPrefab; // Prefab efektu rozprysku
    private bool hasSplashed = false; // Flaga zapobiegająca ponownemu uruchomieniu
    private ParticleSystem bananaParticles; // System cząsteczek
    private AudioSource bananaAudio; // Źródło dźwięku

    void Start()
    {
        // Uzyskanie dostępu do systemu cząsteczek i audio z obiektu banana
        bananaParticles = GetComponent<ParticleSystem>();
        bananaAudio = GetComponent<AudioSource>();
    }

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

        // Odpalamy system cząsteczek (jeśli jest przypisany)
        if (bananaParticles != null)
        {
            bananaParticles.Play();
        }

        // Odtwarzanie dźwięku (jeśli jest przypisany)
        if (bananaAudio != null)
        {
            bananaAudio.Play();
        }

        // Wyłączenie renderowania i kolizji banana
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Collider collider = GetComponent<Collider>();

        if (mesh != null) mesh.enabled = false;
        if (collider != null) collider.enabled = false;

        Destroy(gameObject, 2); // Usuń banana po 2 sekundach
    }
}
