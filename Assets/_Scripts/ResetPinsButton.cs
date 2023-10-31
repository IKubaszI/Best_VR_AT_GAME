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

    private void Start()
    {
       
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            pinPositions.Add(pin.transform.position);
        }

        
        GameObject ball = GameObject.FindGameObjectWithTag("Balling");
        if (ball != null)
        {
            ballPosition = ball.transform.position;
        }
    }

    public void ResetPinsAndBall()
    {
        
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pins)
        {
            Destroy(pin);
        }

        
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        
        foreach (Vector3 position in pinPositions)
        {
            Instantiate(pinPrefab, position, Quaternion.identity);
        }

        
        currentBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
    }
}
