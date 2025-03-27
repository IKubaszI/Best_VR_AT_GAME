using UnityEngine;

public class ExplodableBomb : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public AudioSource explosionSound;

    private bool hasExploded = false;

    public void OnSliced()
    {
        if (hasExploded) return;
        hasExploded = true;

        Debug.Log(" Bomba przecięta!");

        // Efekt wybuchu
        if (explosionEffect != null)
        {
            explosionEffect.transform.SetParent(null);
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
        }

        // Dźwięk
        if (explosionSound != null)
            explosionSound.Play();

        // Odejmij życie
        LivesManager.Instance?.LoseLife();

        // Reset combo
        ComboManager.Instance?.ResetCombo();

        // Wyłącz rendery i kolizję
        if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 2f);
    }
}
