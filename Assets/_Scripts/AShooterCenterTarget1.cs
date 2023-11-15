using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AShooterCenterTarget1 : MonoBehaviour, IHittable
{

 public int scoreOnHit1 = 0; 

    [SerializeField]
    private AudioSource audioSource;


    public void GetHit()
    {
       
         ShooterGetPoint1 getPointScript1 = FindObjectOfType<ShooterGetPoint1>();
            if (getPointScript1 != null)
            {
                getPointScript1.AddPoints(scoreOnHit1);
                audioSource.Play();
                
            }
       
    }

   
}