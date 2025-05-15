using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("UI do wyświetlania podczas ładowania")]
    public GameObject loadingScreenCanvas;
    public Slider progressBar;
    public Text progressText;

    void Awake()
    {
        // zapewniamy, że canvas jest wył.)
        loadingScreenCanvas.SetActive(false);
    }

    /// <summary>
    /// Pokaż loading UI i zaktualizuj suwak/procent
    /// </summary>
    public void Show()
    {
        loadingScreenCanvas.SetActive(true);
    }

    public void Hide()
    {
        loadingScreenCanvas.SetActive(false);
    }

    /// <summary>
    /// Ustaw postęp od 0 do 1
    /// </summary>
    public void SetProgress(float progress)
    {
        progressBar.value = progress;
        progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
    }
}
