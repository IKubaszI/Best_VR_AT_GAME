// GameManager_Menu.cs
using UnityEngine;
using UnityEngine.SceneManagement;
// potrzebne do XROrigin
using Unity.XR.CoreUtils;

public class GameManager_Menu : MonoBehaviour
{
    public static GameManager_Menu Instance { get; private set; }

    [Header("Ustawienia sceny")]
    [Tooltip("Nazwa sceny, którą przeładujesz (dokładnie tak samo jak w Build Settings)")]
    public string mainSceneName = "ArcheryScene";

    [HideInInspector] public Vector3 spawnPosition;
    [HideInInspector] public Vector3 spawnEulerAngles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != mainSceneName) 
            return;

        // Szukamy XR Origin
        var xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
        {
            xrOrigin.transform.position    = spawnPosition;
            xrOrigin.transform.eulerAngles = spawnEulerAngles;
            return;
        }

        Debug.LogError($"Wczytano {mainSceneName}, ale nie znalazłem XROrigin w scenie.");
    }
}
