using UnityEngine;

public class BananaGroundHandler : MonoBehaviour
{
    [Header("Efekty upadku")]
    public ParticleSystem groundEffect;
    public AudioSource groundSound;

    private bool hasSplashed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasSplashed && other.CompareTag("Ground"))
        {
            HandleGroundImpact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasSplashed && collision.gameObject.CompareTag("Ground"))
        {
            HandleGroundImpact();
        }
    }

    void HandleGroundImpact()
    {
        hasSplashed = true;
        Debug.Log("FlyingBanana: uderzył o ziemię!");

        // Resetuj combo
        ComboManager.Instance?.ResetCombo();

        // Odpal cząsteczki
        if (groundEffect != null)
        {
            groundEffect.transform.SetParent(null);
            groundEffect.transform.position = transform.position;
            groundEffect.Play();
        }

        // Odpal dźwięk
        if (groundSound != null)
            groundSound.Play();

        // Wyłącz render i kolizję
        if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 2f);
    }
}
