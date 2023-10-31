using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Dodaj using do namespace Input System

public class ResetScene : MonoBehaviour
{
    private void Update()
    {
        // Sprawd�, czy u�ytkownik nacisn�� klawisz "R" na klawiaturze za pomoc� Input System
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Resetuj scen�
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
