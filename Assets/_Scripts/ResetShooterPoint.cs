using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShooterPoint : MonoBehaviour
{
    public target_strzelnica targetScript; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Trafiono w tarczę!");
            if (targetScript != null)
            {
                targetScript.scoreShooter = 0;
                targetScript.UpdateScoreText();
            }
            else
            {
                Debug.LogError("Nie znaleziono skryptu target_strzelnica.");
            }
        }
    }
}
