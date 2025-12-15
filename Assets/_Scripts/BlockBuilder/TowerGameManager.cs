using UnityEngine;
using TMPro;

public enum GameState
{
    WaitingToStart,
    Playing,
    CheckingStability,
    GameEnded
}

public class TowerGameManager : MonoBehaviour
{
    [Header("UI References - 3D World Text")]
    public TextMeshPro heightText;          // Zmiana z TextMeshProUGUI na TextMeshPro
    public TextMeshPro timerText;           // Zmiana z TextMeshProUGUI na TextMeshPro
    public TextMeshPro instructionText;     // Zmiana z TextMeshProUGUI na TextMeshPro
    public TextMeshPro gameStateText;       // Zmiana z TextMeshProUGUI na TextMeshPro
    
    [Header("Game Settings")]
    public float gameTime = 120f;
    public float stabilityCheckTime = 5f;
    
    [Header("References")]
    public Transform buildSurface;
    public PlatformController platformController;
    
    private GameState currentState = GameState.WaitingToStart;
    private float remainingTime;
    private float highestPoint = 0f;
    private float surfaceHeight;
    
    void Start()
    {
        surfaceHeight = buildSurface.position.y;
        SetGameState(GameState.WaitingToStart);
    }
    
    void Update()
    {
        switch (currentState)
        {
            case GameState.WaitingToStart:
                CheckTowerHeight();
                UpdateHeightUI();
                break;
            case GameState.Playing:
                remainingTime -= Time.deltaTime;
                CheckTowerHeight();
                UpdateUI();
                if (remainingTime <= 0)
                {
                    SetGameState(GameState.CheckingStability);
                }
                break;
            case GameState.CheckingStability:
                remainingTime -= Time.deltaTime;
                CheckTowerHeight();
                UpdateHeightUI();
                if (remainingTime <= 0)
                {
                    SetGameState(GameState.GameEnded);
                }
                break;
            case GameState.GameEnded:
                CheckTowerHeight();
                UpdateHeightUI();
                break;
        }
    }
    
    void SetGameState(GameState newState)
    {
        currentState = newState;
        
        switch (currentState)
        {
            case GameState.WaitingToStart:
                if (gameStateText != null) gameStateText.text = "READY TO START";
                if (instructionText != null) instructionText.text = "Press START button to begin building!";
                if (timerText != null) timerText.text = "Press START";
                break;
                
            case GameState.Playing:
                remainingTime = gameTime;
                if (gameStateText != null) gameStateText.text = "BUILDING TIME";
                if (instructionText != null) instructionText.text = "Build the highest tower!";
                break;
                
            case GameState.CheckingStability:
                remainingTime = stabilityCheckTime;
                if (gameStateText != null) gameStateText.text = "CHECKING STABILITY";
                if (instructionText != null) instructionText.text = "Don't touch anything!";
                if (timerText != null) timerText.text = "Checking...";
                break;
                
            case GameState.GameEnded:
                float finalHeight = Mathf.Max(0, highestPoint - surfaceHeight);
                if (gameStateText != null) gameStateText.text = "GAME COMPLETED";
                if (instructionText != null) instructionText.text = $"Final height: {finalHeight:F2}m\nPress RESTART!";
                if (timerText != null) timerText.text = "FINISHED";
                break;
        }
    }
    
    void CheckTowerHeight()
    {
        highestPoint = surfaceHeight;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        
        foreach (GameObject block in blocks)
        {
            if (block.transform.position.y > highestPoint)
            {
                Rigidbody rb = block.GetComponent<Rigidbody>();
                if (rb != null && rb.velocity.magnitude < 0.5f)
                {
                    highestPoint = block.transform.position.y;
                }
            }
        }
    }
    
    void UpdateUI()
    {
        UpdateHeightUI();
        UpdateTimerUI();
    }
    
    void UpdateHeightUI()
    {
        if (heightText != null)
        {
            float towerHeight = Mathf.Max(0, highestPoint - surfaceHeight);
            heightText.text = "Height: " + towerHeight.ToString("F2") + "m";
        }
    }
    
    void UpdateTimerUI()
    {
        if (currentState == GameState.Playing && timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
    
    // Metody dla Twoich przycisków ButtonVR
    public void StartGame()
    {
        if (currentState == GameState.WaitingToStart)
        {
            SetGameState(GameState.Playing);
        }
    }
    
    public void RestartGame()
    {
        // Usuń wszystkie klocki
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }
        
        // Reset platformy
        if (platformController != null)
        {
            platformController.ResetToStartPosition();
        }
        
        SetGameState(GameState.WaitingToStart);
    }
    
    public void StartOrRestartGame()
    {
        if (currentState == GameState.WaitingToStart)
        {
            StartGame();
        }
        else if (currentState == GameState.GameEnded)
        {
            RestartGame();
        }
    }
}
