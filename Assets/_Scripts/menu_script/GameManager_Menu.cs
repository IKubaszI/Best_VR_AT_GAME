// GameManager_Menu.cs
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager_Menu : MonoBehaviour
{
    public static GameManager_Menu Instance { get; private set; }

    [HideInInspector] public string minigameSceneName;
    [HideInInspector] public string teleportTargetId;

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

        // 1. Znajdź obiekt TeleportTarget o podanym ID
        var target = FindObjectsOfType<TeleportTarget>()
            .FirstOrDefault(t => t.targetId == teleportTargetId);
        if (target == null)
        {
            Debug.LogError(
                $"TeleportTarget o ID '{teleportTargetId}' nie znaleziono w scenie '{scene.name}'."
            );
            return;
        }

        // 2. Spróbuj użyć XR-owego TeleportationProvider
        var tpProvider = FindObjectOfType<TeleportationProvider>();
        if (tpProvider != null)
        {
            var req = new TeleportRequest
            {
                destinationPosition = target.transform.position,
                destinationRotation = target.transform.rotation,
                matchOrientation    = MatchOrientation.TargetUpAndForward
            };
            tpProvider.QueueTeleportRequest(req);
            return;
        }

        // 3. Fallback: direct move + rotate (jeśli nie ma TPProvider)
        var xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
        {
            xrOrigin.transform.SetPositionAndRotation(
                target.transform.position,
                target.transform.rotation
            );
            return;
        }

        Debug.LogError(
            $"Brak TeleportationProvider i XROrigin w scenie '{scene.name}' – nie udało się teleportować."
        );
    }
}
