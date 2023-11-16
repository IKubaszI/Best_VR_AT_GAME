using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShooterPoint : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    public target_strzelnica targetScript; 
    public DisappearManager disappearManager;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Trafiono w tarczę!");
            if (targetScript != null)
            {
                targetScript.scoreShooter = 0;
                disappearManager.ResetPosition();
                targetScript.UpdateScoreText();
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Nie znaleziono skryptu target_strzelnica.");
            }
        }
    }
}
