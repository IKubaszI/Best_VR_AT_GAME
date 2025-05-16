using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
    [Header("Referencje do UI")]
    public GameObject loadingScreenCanvas; 
    public Slider    progressBar;          
    public Text      progressText;         

    void Awake()
    {
        loadingScreenCanvas.SetActive(false);
    }

    public void Show() => loadingScreenCanvas.SetActive(true);
    public void Hide() => loadingScreenCanvas.SetActive(false);

    public void SetProgress(float p)
    {
        progressBar.value = Mathf.Clamp01(p);
        progressText.text = Mathf.RoundToInt(p * 100f) + "%";
    }
}
