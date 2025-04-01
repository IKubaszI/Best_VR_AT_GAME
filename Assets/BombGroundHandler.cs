using UnityEngine;

public class BombGroundHandler : MonoBehaviour
{
    [Header("Efekty uderzenia bomby")]
    public ParticleSystem groundEffect;
    public AudioSource groundSound;

    private bool hasExploded = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasExploded && other.CompareTag("Ground") && !WasCut())
        {
            HandleGroundImpact();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Ground") && !WasCut())
        {
            HandleGroundImpact();
        }
    }

    bool WasCut()
    {
        var bomb = GetComponent<ExplodableBomb>();
        return bomb != null && bomb.wasCut;
    }

    void HandleGroundImpact()
    {
        hasExploded = true;
        Debug.Log(" Bomba uderzyła o ziemię!");

        if (groundEffect != null)
        {
            groundEffect.transform.SetParent(null);
            groundEffect.transform.position = transform.position;
            groundEffect.Play();
        }

        if (groundSound != null)
            groundSound.Play();

        if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 0.4f);
    }
}
