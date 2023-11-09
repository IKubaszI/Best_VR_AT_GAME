using System.Collections;
using UnityEngine;

public class DisappearManager : MonoBehaviour
{
    public GameObject target;

   
    public void DisappearAndMove()
    {
       

        float randomZ = Random.Range(-7.0f, -2.0f); // Losowa koordynata Z od -2 do -7
        float randomY = Random.Range(1.0f, 3.0f); // Losowa koordynata Z od -2 do -7
        float randomX = Random.Range(17.0f, 20.0f); // Losowa koordynata Z od -2 do -7
        transform.position = new Vector3(randomX, randomY, randomZ);
    }

    
}
