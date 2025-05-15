using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class DiskSelector : MonoBehaviour
{
    [Header("Dokładna nazwa sceny minigry (z Build Settings)")]
    public string minigameSceneName;

    [Header("ID punktu startowego w tej scenie")]
    [Tooltip("Musisz mieć w docelowej scenie obiekt z TeleportTarget.targetId = tej wartości")]
    public string teleportTargetId;
}
