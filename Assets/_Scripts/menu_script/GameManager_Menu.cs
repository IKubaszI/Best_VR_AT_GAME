using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Teleportation; // tylko dla TeleportRequest

public class GameManager_Menu : MonoBehaviour
{
    public static GameManager_Menu Instance { get; private set; }

    [HideInInspector] public string   minigameSceneName;
    [HideInInspector] public string   teleportTargetId;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != minigameSceneName)
            return;

        // 1) Znajdź pointa
        var target = FindObjectsOfType<TeleportTarget>()
            .FirstOrDefault(t => t.targetId == teleportTargetId);
        if (target == null)
        {
            Debug.LogError($"Nie znaleziono TeleportTarget o ID '{teleportTargetId}' w scenie '{scene.name}'");
            return;
        }

        // 2) Spróbuj teleportacji XR (ładnie obsłuży rotację i fade)
        var tpProvider = FindObjectOfType<TeleportationProvider>();
        if (tpProvider != null)
        {
            var req = new TeleportRequest {
                destinationPosition = target.transform.position,
                destinationRotation = target.transform.rotation,
                matchOrientation    = MatchOrientation.TargetUpAndForward
            };
            tpProvider.QueueTeleportRequest(req);
            return;
        }

        // 3) Fallback na bezpośrednie ustawienie, jeśli nie masz TeleportationProvider
        var xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
        {
            xrOrigin.transform.SetPositionAndRotation(
                target.transform.position,
                target.transform.rotation
            );
            return;
        }

        Debug.LogError("Brak TeleportationProvider i XROrigin – nie udało się teleportować!");
    }
}
