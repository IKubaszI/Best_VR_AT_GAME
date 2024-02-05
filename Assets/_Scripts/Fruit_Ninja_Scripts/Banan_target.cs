using UnityEngine;
using TMPro;

public class Banan_target : MonoBehaviour
{
    public BananDisappearManager disappearManager;
    public TMP_Text scoreText;

    public int scoreShooter = 0;
    [SerializeField]
    private AudioSource audioSource;

    void Start()
    {
        disappearManager = transform.parent.GetComponent<BananDisappearManager>();
        scoreText.text = "Twoj wynik: " + scoreShooter.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Katana"))
        {
            Debug.Log("Trafiono w tarczę!");
            AddPoints(10); // Dodaj 100 pkt za trafienie
            disappearManager.DisappearAndMove();
            audioSource.Play();
        }
    }

    void AddPoints(int points)
    {
        scoreShooter += points;
        UpdateScoreText();
    }

    public void UpdateScoreText() // Zmiana na public
    {
        scoreText.text = "Twoj wynik: " + scoreShooter.ToString();
    }
}
