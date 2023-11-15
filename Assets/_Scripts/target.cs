using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public int scoreOnHit = 0; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow") == true)
        {
        
            Debug.Log("Strzała trafiła w cel!");
            GetPoint getPointScript = FindObjectOfType<GetPoint>();
            if (getPointScript != null)
            {
                getPointScript.AddPoints(scoreOnHit);
            }
        }
          
        
    }
}
