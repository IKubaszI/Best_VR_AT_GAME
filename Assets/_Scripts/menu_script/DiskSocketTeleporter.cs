using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

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
        var disk = args.interactableObject.transform.GetComponent<DiskSelector>();
        if (disk == null) return;

        var gm = GameManager_Menu.Instance;
        if (gm == null)
        {
            Debug.LogError("GameManager_Menu.Instance == null! Upewnij się, że masz go w scenie menu.");
            return;
        }

        // zapisujemy parametry
        gm.minigameSceneName = disk.minigameSceneName;
        gm.spawnPosition     = disk.spawnPosition;
        gm.spawnEulerAngles  = disk.spawnEulerAngles;

        // przeładowujemy na minigierkę
        SceneManager.LoadScene(disk.minigameSceneName, LoadSceneMode.Single);
    }
}
