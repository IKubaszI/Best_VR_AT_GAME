using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Dodaj using do namespace Input System

public class ResetScene : MonoBehaviour
{
   private static float heightChangeAmount = 0.05f; // Ilo�� zmiany wysoko�ci przy jednym naci�ni�ciu przycisku

    private void Update()
    {
        // Sprawd�, czy u�ytkownik nacisn�� klawisz "R" na klawiaturze za pomoc� Input System
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Resetuj scen�
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AdjustHeight(heightChangeAmount);
        }

        // Przy naci�ni�ciu przycisku "-" zmniejsz wysoko�� XR Origin
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            AdjustHeight(-heightChangeAmount);
        }
    }

    void AdjustHeight(float amount)
    {
        // Znajd� XR Rig (XR Origin) w scenie
        GameObject xrRig = GameObject.Find("XR Origin"); // Zmie� "XR Rig" na nazw� swojego obiektu XR Origin

        if (xrRig != null)
        {
            // Zmiana pozycji XR Origin w osi Y
            xrRig.transform.position += new Vector3(0.0f, amount, 0.0f);
        }
        else
        {
            Debug.LogError("Nie mo�na znale�� obiektu XR Rig w scenie.");
        }
    }
}
