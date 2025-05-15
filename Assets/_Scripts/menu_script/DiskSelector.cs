using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class DiskSelector : MonoBehaviour
{
    [Header("Nazwa sceny minigry (dokładnie jak w Build Settings)")]
    public string minigameSceneName;

    [Header("Koordynaty startowe w nowej scenie")]
    public Vector3 spawnPosition;
    public Vector3 spawnEulerAngles;
}
