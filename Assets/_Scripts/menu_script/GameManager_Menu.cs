using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager_Menu : MonoBehaviour
{
    public static GameManager_Menu Instance { get; private set; }

    [HideInInspector] public string    minigameSceneName;
    [HideInInspector] public Vector3   spawnPosition;
    [HideInInspector] public Vector3   spawnEulerAngles;

    XROrigin             _persistentOrigin;
    XRInteractionManager _persistentInteractionManager;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // zachowaj swój rig i manager z menu
        _persistentOrigin              = FindObjectOfType<XROrigin>();
        _persistentInteractionManager  = FindObjectOfType<XRInteractionManager>();

        if (_persistentOrigin != null)
            DontDestroyOnLoad(_persistentOrigin.gameObject);
        if (_persistentInteractionManager != null)
            DontDestroyOnLoad(_persistentInteractionManager.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // czy to ta minigierka?
        if (scene.name != minigameSceneName) return;

        // usuń wszelkie nowe rigs/manager’y
        foreach (var origin in FindObjectsOfType<XROrigin>())
            if (origin != _persistentOrigin)
                Destroy(origin.gameObject);

        foreach (var mgr in FindObjectsOfType<XRInteractionManager>())
            if (mgr != _persistentInteractionManager)
                Destroy(mgr.gameObject);

        // teleportuj persistentny rig
        if (_persistentOrigin != null)
        {
            _persistentOrigin.transform.position    = spawnPosition;
            _persistentOrigin.transform.eulerAngles = spawnEulerAngles;
        }
        else Debug.LogError("Persistent XROrigin jest null!");
    }
}
