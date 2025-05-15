using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class DiskSelector : MonoBehaviour
{
    [Header("Minigierka i punkt startowy")]
    [Tooltip("Dokładna nazwa sceny minigierki w Build Settings")]
    public string minigameSceneName;

    [Tooltip("Współrzędne (X,Y,Z), do których przeniesiemy rig w minigierce")]
    public Vector3 spawnPosition;

    [Tooltip("Euler (X,Y,Z) rotacji rig’a w minigierce")]
    public Vector3 spawnEulerAngles;
}
