using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BetterBowStringController : MonoBehaviour
{
    [Header("Do renderowania linii cięciwy")]
    [SerializeField] private BowString bowStringRenderer;

    [Header("Obiekty cięciwy")]
    [SerializeField] private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [Header("Maksymalne naciągnięcie")]
    [SerializeField] private float bowStringStretchLimit = 0.3f;

    [Header("Dźwięk cięciwy")]
    [SerializeField] private AudioSource audioSource;

    [Header("Obsługa nakładania/strzału")]
    [SerializeField] private ArrowSocket arrowSocket; // ← tu podpinamy ArrowSocket z triggerem cięciwy

    private XRGrabInteractable interactable;
    private Transform interactor;
    private float strength, previousStrength;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
        // Tu możesz dodać np. dźwięk naciągania itp
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        // TU: strzał!
        if (arrowSocket != null)
            arrowSocket.ReleaseArrow(strength);

        strength = 0;
        previousStrength = 0;
        audioSource.pitch = 1;
        audioSource.Stop();

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void Update()
    {
        if (interactor != null)
        {
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position);
            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);
            previousStrength = strength;

            if (midPointLocalSpace.z < 0 && midPointLocalZAbs < bowStringStretchLimit)
            {
                if (!audioSource.isPlaying && strength <= 0.01f)
                    audioSource.Play();

                strength = Remap(midPointLocalZAbs, 0, bowStringStretchLimit, 0, 1);
                midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
            }
            else if (midPointLocalSpace.z < 0 && midPointLocalZAbs >= bowStringStretchLimit)
            {
                audioSource.Pause();
                strength = 1;
                midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
            }
            else if (midPointLocalSpace.z >= 0)
            {
                audioSource.pitch = 1;
                audioSource.Stop();
                strength = 0;
                midPointVisualObject.localPosition = Vector3.zero;
            }

            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
