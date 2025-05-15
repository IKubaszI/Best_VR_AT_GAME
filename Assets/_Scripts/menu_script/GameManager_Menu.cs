// GameManager_Menu.cs
using UnityEngine;
using UnityEngine.SceneManagement;

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
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != mainSceneName) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position     = spawnPosition;
            player.transform.eulerAngles  = spawnEulerAngles;
        }
        else
        {
            Debug.LogError($"Wczytano scenę {mainSceneName}, ale nie znaleziono obiektu z tagiem 'Player'.");
        }
    }
}
