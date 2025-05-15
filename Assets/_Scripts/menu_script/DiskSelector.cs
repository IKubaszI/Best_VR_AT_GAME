// DiskSelector.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class DiskSelector : MonoBehaviour
{
    [Header("Koordynaty punktu startowego")]
    [Tooltip("Współrzędne (X, Y, Z) miejsca, na które chcesz przeteleportować gracza")]
    public Vector3 spawnPosition;

    [Tooltip("Euler (X, Y, Z) rotacji, do której gracz zostanie ustawiony")]
    public Vector3 spawnEulerAngles;
}
