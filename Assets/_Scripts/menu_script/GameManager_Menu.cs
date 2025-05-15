using UnityEngine;
using UnityEngine.SceneManagement;
// żeby znaleźć XROrigin
using Unity.XR.CoreUtils;

public class GameManager_Menu : MonoBehaviour
{
    public static GameManager_Menu Instance { get; private set; }

    [HideInInspector] public string  minigameSceneName;
    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public Vector3 spawnEulerAngles;

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
        // czy to scena minigierki?
        if (scene.name != minigameSceneName) return;

        // znajdź nowy XR Origin w tej scenie
        var xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin == null)
        {
            Debug.LogError($"Wczytano '{scene.name}', ale nie znalazłem XROrigin!");
            return;
        }

        // teleportujemy nowy rig w minigierce
        xrOrigin.transform.position    = spawnPosition;
        xrOrigin.transform.eulerAngles = spawnEulerAngles;
    }
}
