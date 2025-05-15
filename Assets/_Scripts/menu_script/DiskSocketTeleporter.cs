// DiskSocketTeleporter.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSocketInteractor))]
public class DiskSocketTeleporter : MonoBehaviour
{
    private XRSocketInteractor _socket;

    private void Awake()
    {
        _socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        _socket.selectEntered.AddListener(OnDiskPlaced);
    }

    private void OnDisable()
    {
        _socket.selectEntered.RemoveListener(OnDiskPlaced);
    }

    private void OnDiskPlaced(SelectEnterEventArgs args)
    {
        // Pobierz ustawienia z wrzuconej dyskietki
        var disk = args.interactableObject.transform.GetComponent<DiskSelector>();
        if (disk == null)
            return;

        // Zapisz w GameManager_Menu nazwę sceny i ID punktu
        var gm = GameManager_Menu.Instance;
        gm.minigameSceneName = disk.minigameSceneName;
        gm.teleportTargetId  = disk.teleportTargetId;

        // Uruchom asynchroniczne ładowanie z loading screenem
        gm.LoadMinigame();
    }
}
