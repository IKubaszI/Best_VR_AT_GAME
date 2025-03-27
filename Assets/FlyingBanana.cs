using UnityEngine;

public class FlyingBanana : MonoBehaviour
{
    private bool hasBeenHit = false;

    [Header("Efekty cięcia")]
    public ParticleSystem cutEffect;
    public AudioSource cutSound;

    [Header("Opcje punktacji")]
    public bool disablePoints = false;

    public void OnSliced()
    {
        if (hasBeenHit) return;

        hasBeenHit = true;

        Debug.Log("FlyingBanana: przecięty");

        if (cutEffect != null)
        {
            cutEffect.transform.SetParent(null);
            cutEffect.transform.position = transform.position;
            cutEffect.Play();
        }

        if (cutSound != null)
            cutSound.Play();

        if (!disablePoints && FruitScoreManager.Instance != null)
            FruitScoreManager.Instance.AddPoints(10);
    }
}
