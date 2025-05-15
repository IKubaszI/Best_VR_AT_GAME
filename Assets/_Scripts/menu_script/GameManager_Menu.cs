using System.Collections;
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

    // referencja na komponent LoadingScreenManager (zgłoś w Inspectorze!)
    [Header("Referencja do Loading Screen UI")]
    public LoadingScreenManager loadingScreen;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // canvasy i UI razem z GameManager są trwałe
        DontDestroyOnLoad(loadingScreen.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Wywołaj zamiast SceneManager.LoadScene
    /// </summary>
    public void LoadMinigame()
    {
        StartCoroutine(LoadAsyncWithLoadingScreen(minigameSceneName));
    }

    private IEnumerator LoadAsyncWithLoadingScreen(string sceneName)
    {
        // 1) pokaż UI
        loadingScreen.Show();

        // 2) zacznij ładować asynchronicznie
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        op.allowSceneActivation = false;

        // 3) dopóki się ładuje, aktualizuj slider
        while (!op.isDone)
        {
            // Unity raportuje progress do 0.9f, potem czeka na allowSceneActivation
            float prog = Mathf.Clamp01(op.progress / 0.9f);
            loadingScreen.SetProgress(prog);

            // gdy wczytane w 90%, przepuść scenę
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
            }

            yield return null;
        }

        // 4) ukryj UI
        loadingScreen.Hide();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != minigameSceneName) return;

        // odnajdź TeleportTarget
        var target = FindObjectsOfType<TeleportTarget>()
            .FirstOrDefault(t => t.targetId == teleportTargetId);
        if (target == null)
        {
            Debug.LogError($"TeleportTarget '{teleportTargetId}' nie znaleziono w '{scene.name}'");
            return;
        }

        // teleport XR-owo (jeśli masz TeleportationProvider) albo direct
        var tp = FindObjectOfType<TeleportationProvider>();
        if (tp != null)
        {
            var req = new TeleportRequest
            {
                destinationPosition = target.transform.position,
                destinationRotation = target.transform.rotation,
                matchOrientation    = MatchOrientation.TargetUpAndForward
            };
            tp.QueueTeleportRequest(req);
        }
        else
        {
            var xrOrigin = FindObjectOfType<XROrigin>();
            if (xrOrigin != null)
                xrOrigin.transform.SetPositionAndRotation(
                    target.transform.position,
                    target.transform.rotation
                );
            else
                Debug.LogError("Brak XROrigin – nie mogę teleportować.");
        }
    }
}
