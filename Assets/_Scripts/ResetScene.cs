using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Dodaj using do namespace Input System

public class ResetScene : MonoBehaviour
{
    private void Update()
    {
        // SprawdŸ, czy u¿ytkownik nacisn¹³ klawisz "R" na klawiaturze za pomoc¹ Input System
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Resetuj scenê
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
