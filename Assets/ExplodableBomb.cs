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

        Debug.Log("💥 Bomba przecięta!");

        if (explosionEffect != null)
        {
            explosionEffect.transform.SetParent(null);
            explosionEffect.transform.position = transform.position;
            explosionEffect.Play();
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
