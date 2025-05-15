using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class LevelSelector : MonoBehaviour
{
    [Header("Miejsca startu dla poszczególnych gier")]
    public Transform[] spawnPoints;   // 0=FruitNinjaStart, 1=ArcheryStart, ...

    [Header("XR Rig (gracz)")]
    public GameObject xrRig;          // Twój XR Origin / CameraRig

    private XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(OnDiskInserted);
    }

    private void OnDiskInserted(XRBaseInteractable disk)
    {
        var ld = disk.GetComponent<LevelDisk>();
        if (ld == null) return;

        int idx = ld.levelIndex;
        if (idx < 0 || idx >= spawnPoints.Length) return;

        // teleportujemy gracza
        xrRig.transform.position = spawnPoints[idx].position;
        xrRig.transform.rotation = spawnPoints[idx].rotation;
    }
}
