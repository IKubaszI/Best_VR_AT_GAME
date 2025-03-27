using UnityEngine;

public class BombGroundHandler : MonoBehaviour
{
    [Header("Efekty uderzenia bomby")]
    public ParticleSystem groundEffect;
    public AudioSource groundSound;

    private bool hasExploded = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && other.CompareTag("Ground"))
        {
            HandleGroundImpact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Ground"))
        {
            HandleGroundImpact();
        }
    }

    void HandleGroundImpact()
    {
        hasExploded = true;
        Debug.Log("💣 Bomba uderzyła o ziemię!");

        // Odtwórz efekt cząsteczek
        if (groundEffect != null)
        {
            groundEffect.transform.SetParent(null);
            groundEffect.transform.position = transform.position;
            groundEffect.Play();
        }

        // Odtwórz dźwięk
        if (groundSound != null)
            groundSound.Play();

        // Wyłącz renderowanie i kolizję
        if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 2f); // niszczy bombę po 2 sek
    }
}
