using System.Collections;
using UnityEngine;

public class DisappearManager : MonoBehaviour
{
    public GameObject target;

    public void DisappearAndMove()
    {
        target.SetActive(false); // Ukryj cel

        float randomZ = Random.Range(-9.0f, -2.0f); // Losowa koordynata Z od -2 do -7
        float randomY = Random.Range(1.0f, 3.0f); 
        float randomX = Random.Range(17.0f, 20.0f); 
        transform.position = new Vector3(randomX, randomY, randomZ);
    }

    IEnumerator DisappearAndAppear()
    {
        //while (true)
       // {
            DisappearAndMove();

            yield return new WaitForSeconds(2.0f); // Poczekaj kolejne 4 sekundy

            target.SetActive(true); // Pokaż cel
        //}
    }
}
