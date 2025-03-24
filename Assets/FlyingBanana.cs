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
        if (!hasBeenHit)
        {
            hasBeenHit = true;

            Debug.Log("FlyingBanana: przecięty");

            // Efekt cząsteczek
            if (cutEffect != null)
            {
                cutEffect.transform.SetParent(null);
                cutEffect.transform.position = transform.position;
                cutEffect.Play();
            }

            // Dźwięk
            if (cutSound != null)
                cutSound.Play();

            // Punkty
            if (!disablePoints && FruitScoreManager.Instance != null)
            {
                FruitScoreManager.Instance.AddPoints(10);
            }
        }
        else
        {
            Debug.Log("FlyingBanana: już wcześniej przecięty");
        }
    }
}
