using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPinsButton : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject ballPrefab;
    private List<Vector3> pinPositions = new List<Vector3>();
    private Vector3 ballPosition;
    private GameObject currentBall;

    private ScoreManager scoreManager;

    private void Start()
    {
        // Zapamiętujemy pozycje kręgli
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            pinPositions.Add(pin.transform.position);
        }

        // Zapamiętujemy pozycję kuli
        GameObject ball = GameObject.FindGameObjectWithTag("Balling");
        if (ball != null)
        {
            ballPosition = ball.transform.position;
        }

        // Znajdź ScoreManager w scenie
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void ResetPinsAndBall()
    {
        // Zniszcz stare kręgle
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            Destroy(pin);
        }

        // Zniszcz starą kulę
        if (currentBall != null)
        {
            Destroy(currentBall);
        }
        else
        {
            GameObject existingBall = GameObject.FindGameObjectWithTag("Balling");
            if (existingBall != null) Destroy(existingBall);
        }

        // Odrodź kręgle
        foreach (Vector3 position in pinPositions)
        {
            Instantiate(pinPrefab, position, Quaternion.identity);
        }

        // Odrodź kulę
        currentBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);

        // Zresetuj punktację
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
    }
}
