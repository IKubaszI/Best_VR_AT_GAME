using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Dodaj using do namespace Input System

public class ResetScene : MonoBehaviour
{
    public float heightChangeAmount = 0.1f; // Iloœæ zmiany wysokoœci przy jednym naciœniêciu przycisku

    private void Update()
    {
        // SprawdŸ, czy u¿ytkownik nacisn¹³ klawisz "R" na klawiaturze za pomoc¹ Input System
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Resetuj scenê
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            AdjustHeight(heightChangeAmount);
        }

        // Przy naciœniêciu przycisku "-" zmniejsz wysokoœæ XR Origin
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            AdjustHeight(-heightChangeAmount);
        }
    }

    void AdjustHeight(float amount)
    {
        // ZnajdŸ XR Rig (XR Origin) w scenie
        GameObject xrRig = GameObject.Find("XR Origin"); // Zmieñ "XR Rig" na nazwê swojego obiektu XR Origin

        if (xrRig != null)
        {
            // Zmiana pozycji XR Origin w osi Y
            xrRig.transform.position += new Vector3(0.0f, amount, 0.0f);
        }
        else
        {
            Debug.LogError("Nie mo¿na znaleŸæ obiektu XR Rig w scenie.");
        }
    }
}
