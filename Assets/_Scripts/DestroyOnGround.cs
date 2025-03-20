using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Banan spadł na ziemię! Nie dodajemy punktów.");
            Destroy(gameObject); // Niszczy obiekt
        }
    }
}
