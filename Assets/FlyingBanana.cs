using UnityEngine;
using TMPro;

public class FlyingBanana : MonoBehaviour
{
    private bool hasBeenHit = false;

    [Header("Efekty cięcia")]
    public ParticleSystem cutEffect;
    public AudioSource cutSound;

    [Header("Opcje punktacji")]
    public bool disablePoints = false;
    public static TMP_Text scoreText;

    private int scoreShooter = 0;

    public void OnSliced()
    {
        if (!hasBeenHit)
        {
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

            if (!disablePoints)
                AddPoints(10);
        }
        else
        {
            Debug.Log("FlyingBanana: już wcześniej przecięty");
        }
    }

    private void AddPoints(int points)
    {
        scoreShooter += points;

        if (scoreText != null)
            scoreText.text = "Twoj wynik: " + scoreShooter.ToString();
        else
            Debug.LogWarning("FlyingBanana: Brak przypisanego scoreText!");
    }
}
