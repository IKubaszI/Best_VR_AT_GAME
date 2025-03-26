using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ResetCombo()
    {
        Debug.Log(" Mnożnik zresetowany!");
        // Placeholder – logika combo do dodania później
    }
}
