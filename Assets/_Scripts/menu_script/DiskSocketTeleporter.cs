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
    {
        _socketInteractor.selectEntered.AddListener(OnDiskPlaced);
    }

    private void OnDisable()
    {
        _socketInteractor.selectEntered.RemoveListener(OnDiskPlaced);
    }

    private void OnDiskPlaced(SelectEnterEventArgs args)
    {
        var disk = args.interactableObject.transform.GetComponent<DiskSelector>();
        if (disk == null) return;

        // Pobieramy singleton GameManager_Menu
        var gm = GameManager_Menu.Instance;
        if (gm == null)
        {
            Debug.LogError("GameManager_Menu.Instance jest null! Sprawdź, czy masz w scenie obiekt z GameManager_Menu i DontDestroyOnLoad.");
            return;
        }

        // Ustawiamy spawnPosition i spawnEulerAngles
        gm.spawnPosition    = disk.spawnPosition;
        gm.spawnEulerAngles = disk.spawnEulerAngles;

        // Przeładowujemy główną scenę
        SceneManager.LoadScene(gm.mainSceneName);
    }
}
