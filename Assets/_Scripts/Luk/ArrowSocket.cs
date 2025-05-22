using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSocket : MonoBehaviour
{
    [Header("Ukryta strzała na cięciwie")]
    [SerializeField] private GameObject midPointVisual;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Arrow"))
            return;

        XRGrabInteractable grabInteractable = other.GetComponent<XRGrabInteractable>();
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting as XRBaseInteractor;
            if (interactor != null && interactor.interactionManager != null)
                interactor.interactionManager.SelectExit(interactor, grabInteractable);

            Destroy(grabInteractable.gameObject);

            // Pokazujemy strzałę na cięciwie
            Debug.Log("Pokazuję wizualną strzałę na cięciwie!");
            midPointVisual.SetActive(true);
        }
    }
}
