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
        if (hasBeenHit)
        {
            Debug.Log("FlyingBanana: już wcześniej przecięty");
            return;
        }

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

        // Punkty z mnożnikiem
        if (!disablePoints && FruitScoreManager.Instance != null)
        {
            int basePoints = 10;
            int multiplier = ComboManager.Instance != null ? ComboManager.Instance.GetCurrentMultiplier() : 1;
            int totalPoints = basePoints * multiplier;

            FruitScoreManager.Instance.AddPoints(totalPoints);

            // Zwiększ combo
            ComboManager.Instance?.OnFruitSliced();
        }
    }
}
