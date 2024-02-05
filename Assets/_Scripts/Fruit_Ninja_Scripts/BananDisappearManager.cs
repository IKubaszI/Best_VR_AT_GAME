using System.Collections;
using UnityEngine;

public class BananDisappearManager : MonoBehaviour
{
    public GameObject target;

    public void DisappearAndMove()
    {
        float randomZ = Random.Range(-14.0f, -16.0f); // Losowa koordynata Z od -2 do -7
        float randomY = 1.4f;
        float randomX = Random.Range(3.0f, 6.0f);
        transform.position = new Vector3(randomX, randomY, randomZ);
    }
    public void ResetPosition()
    {
        float randomZ = -15.5f; // Losowa koordynata Z od -2 do -7
        float randomY = 1.4f;
        float randomX = 5.24f;
        transform.position = new Vector3(randomX, randomY, randomZ);
    }

}
