// ResetShooterPoint.cs
using UnityEngine;

public class ResetShooterPoint : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public target_strzelnica targetScript; 
    public DisappearManager disappearManager;

    /// <summary>
    /// Wywołaj tę metodę z ButtonVR.OnPress
    /// </summary>
    public void ResetOnButton()
    {
        Debug.Log("Trafiono w tarczę (przycisk)!");
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

    // jeśli nadal chcesz reagować na kule, możesz zostawić:
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            ResetOnButton();
    }
}
