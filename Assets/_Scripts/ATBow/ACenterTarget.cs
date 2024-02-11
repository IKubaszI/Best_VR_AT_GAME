using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACenterTarget : MonoBehaviour, IHittable
{

 public int scoreOnHit = 0; 

    [SerializeField]
    private AudioSource audioSource;


    public void GetHit()
    {
       
         GetPoint getPointScript = FindObjectOfType<GetPoint>();
            if (getPointScript != null)
            {
                getPointScript.AddPoints(scoreOnHit);
                audioSource.Play();
                
            }
       
    }

   
}