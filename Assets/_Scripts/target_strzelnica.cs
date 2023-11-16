using UnityEngine;
using TMPro;

public class target_strzelnica : MonoBehaviour
{
    public DisappearManager disappearManager;
    public TMP_Text scoreText;

    public int scoreShooter = 0;

    void Start()
    {
        disappearManager = transform.parent.GetComponent<DisappearManager>();
        scoreText.text = "Wynik: " + scoreShooter.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Trafiono w tarczę!");
            AddPoints(100); // Dodaj 100 pkt za trafienie
            disappearManager.DisappearAndMove();
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
