// DiskSocketTeleporter.cs
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(XRSocketInteractor))]
public class DiskSocketTeleporter : MonoBehaviour
{
    private XRSocketInteractor _socketInteractor;

    private void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
        => _socketInteractor.selectEntered.AddListener(OnDiskPlaced);

    private void OnDisable()
        => _socketInteractor.selectEntered.RemoveListener(OnDiskPlaced);

    private void OnDiskPlaced(SelectEnterEventArgs args)
    {
        var disk = args.interactableObject.transform.GetComponent<DiskSelector>();
        if (disk == null) return;

        var gm = GameManager_Menu.Instance;
        if (gm == null)
        {
            Debug.LogError("GameManager_Menu.Instance jest null! Upewnij się, że masz w scenie obiekt z GameManager_Menu i DontDestroyOnLoad.");
            return;
        }

        gm.spawnPosition    = disk.spawnPosition;
        gm.spawnEulerAngles = disk.spawnEulerAngles;
        SceneManager.LoadScene(gm.mainSceneName);
    }
}
