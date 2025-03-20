using UnityEngine;

public class Pin : MonoBehaviour
{
    private bool isKnockedDown = false;

    void Update()
    {
        // Sprawdź odchylenie kręgla
        if (!isKnockedDown && transform.up.y < 0.5f)
        {
            isKnockedDown = true;
            // Znajdź ScoreManager i dodaj punkt
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }
        }
    }
}
