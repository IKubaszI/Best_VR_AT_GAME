using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSocket : MonoBehaviour
{
    [Header("Wizualna strzała na cięciwie")]
    [SerializeField] private GameObject midPointVisual;

    [Header("Prefab prawdziwej strzały")]
    [SerializeField] private GameObject arrowPrefab;

    [Header("Punkt spawnu strzały")]
    [SerializeField] private Transform arrowSpawnPoint;

    [Header("Maksymalna siła strzału")]
    [SerializeField] private float arrowMaxSpeed = 10f;

    private bool arrowNocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Arrow") || arrowNocked)
            return;

        XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting as XRBaseInteractor;
            if (interactor != null && interactor.interactionManager != null)
                interactor.interactionManager.SelectExit(interactor, grabInteractable);

            Destroy(grabInteractable.gameObject);

            NockArrow();
        }
    }

    public void NockArrow()
    {
        midPointVisual.SetActive(true);
        arrowNocked = true;
        Debug.Log("Strzała nałożona na cięciwę.");
    }

    public void ReleaseArrow(float strength)
    {
        if (!arrowNocked)
            return;

        // Ukryj wizualną strzałę na cięciwie
        midPointVisual.SetActive(false);
        arrowNocked = false;

        // Stwórz fizyczną strzałę
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, midPointVisual.transform.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(midPointVisual.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);

        Debug.Log("Wystrzelono strzałę z siłą: " + strength);
    }
}
