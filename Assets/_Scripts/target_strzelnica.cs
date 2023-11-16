using UnityEngine;
using TMPro;

public class target_strzelnica : MonoBehaviour
{
    public DisappearManager disappearManager;
    public TMP_Text scoreText;

    private int score = 0;

    void Start()
    {
        disappearManager = transform.parent.GetComponent<DisappearManager>();
        scoreText.text = "Wynik: " + score.ToString();
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
        score += points;
        UpdateScoreText();

    }

    void UpdateScoreText()
    {
        scoreText.text = "Twoj wynik: " + score.ToString();
    }
}
