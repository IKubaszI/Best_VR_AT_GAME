using UnityEngine;

public class NeonSlash : MonoBehaviour
{
    private LineRenderer lr;
    private float fadeSpeed = 5f;
    private Color initialColor;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        initialColor = lr.startColor;
    }

    void Update()
    {
        Color faded = Color.Lerp(lr.startColor, Color.clear, Time.deltaTime * fadeSpeed);
        lr.startColor = faded;
        lr.endColor = faded;
    }
}
