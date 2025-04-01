using UnityEngine;

public class ExplodableBomb : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    public AudioSource explosionSound;

    private bool hasExploded = false;

    [HideInInspector] public bool wasCut = false; // Dodane tutaj!

    public void OnSliced()
    {
        if (hasExploded) return;
        hasExploded = true;
        wasCut = true; // Bomba została przecięta

        Debug.Log(" Bomba przecięta!");

        if (explosionEffect != null)
        {
            explosionEffect.transform.SetParent(null);
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
            Destroy(explosionEffect.gameObject, explosionEffect.main.duration + 0.5f);
        }

        if (explosionSound != null)
            explosionSound.Play();

        LivesManager.Instance?.LoseLife();
        ComboManager.Instance?.ResetCombo();

        if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 2f);
    }
}
