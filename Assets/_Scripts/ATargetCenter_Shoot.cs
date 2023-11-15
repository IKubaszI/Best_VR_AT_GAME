using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATargetCenter_Shoot : MonoBehaviour, IHittable
{

 public int scoreOnHit = 0; 
    public void GetHit()
    {
       
         GetPoint_Shooter getPointScript = FindObjectOfType<GetPoint_Shooter>();
            if (getPointScript != null)
            {
                getPointScript.AddPoints(scoreOnHit);
                
                
            }
       
    }

   
}