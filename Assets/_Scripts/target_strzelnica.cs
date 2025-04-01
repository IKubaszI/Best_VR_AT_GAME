using UnityEngine;
using TMPro;

public class target_strzelnica : MonoBehaviour
{
    public DisappearManager disappearManager;
    public TMP_Text scoreText;

    public int scoreShooter = 0;
    private AudioSource audioSource;

    void Start()
    {
        disappearManager = transform.parent.GetComponent<DisappearManager>();
        audioSource = GetComponent<AudioSource>();

        if (scoreText != null)
            scoreText.text = "Twoj wynik: " + scoreShooter.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Trafiono w tarczę!");
            AddPoints(100);
            disappearManager?.DisappearAndMove();
            audioSource?.Play();
        }
    }

    void AddPoints(int points)
    {
        scoreShooter += points;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Twoj wynik: " + scoreShooter.ToString();
    }
}
