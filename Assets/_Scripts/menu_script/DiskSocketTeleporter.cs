using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(XRSocketInteractor))]
public class DiskSocketTeleporter : MonoBehaviour
{
    XRSocketInteractor _socket;
    void Awake() => _socket = GetComponent<XRSocketInteractor>();
    void OnEnable()  => _socket.selectEntered.AddListener(OnDiskPlaced);
    void OnDisable() => _socket.selectEntered.RemoveListener(OnDiskPlaced);

    void OnDiskPlaced(SelectEnterEventArgs args)
    {
        var disk = args.interactableObject.transform.GetComponent<DiskSelector>();
        if (disk == null) return;

        var gm = GameManager_Menu.Instance;
        if (gm == null)
        {
            Debug.LogError("Brak GameManager_Menu.Instance!");
            return;
        }

        // zapamiętujemy parametry
        gm.minigameSceneName = disk.minigameSceneName;
        gm.spawnPosition     = disk.spawnPosition;
        gm.spawnEulerAngles  = disk.spawnEulerAngles;

        // ładujemy minigierkę
        SceneManager.LoadScene(disk.minigameSceneName, LoadSceneMode.Single);
    }
}
