using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSocket : MonoBehaviour
{
    [Header("Wizualna strzała na cięciwie")]
    [SerializeField] private GameObject midPointVisual;

    [Header("Prefab prawdziwej strzały")]
    [SerializeField] private GameObject arrowPrefab;

    [Header("Punkt spawnu strzały (oś Z musi patrzeć tam, gdzie chcesz strzelać)")]
    [SerializeField] private Transform arrowSpawnPoint;

    [Header("Maksymalna prędkość przy strength = 1")]
    [SerializeField] private float arrowMaxSpeed = 15f;

    private bool arrowNocked = false;

    /* ---------- ładowanie strzały ---------- */
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Arrow") || arrowNocked) return;

        if (other.TryGetComponent(out XRGrabInteractable grab) && grab.isSelected)
        {
            var interactor = grab.firstInteractorSelecting as XRBaseInteractor;
            interactor?.interactionManager.SelectExit(interactor, grab);
            Destroy(grab.gameObject);                 // usuwamy placeholder z ręki
        }

        midPointVisual.SetActive(true);
        arrowNocked = true;
    }

    /* ---------- wystrzał; wywołuje BetterBowStringController.ResetBowString ---------- */
    public void ReleaseArrow(float strength)
{
    if (!arrowNocked) return;

    midPointVisual.SetActive(false);
    arrowNocked = false;

    GameObject arrow = Instantiate(
        arrowPrefab,
        arrowSpawnPoint.position,
        arrowSpawnPoint.rotation);

    // KLUCZ: od razu obracamy i uruchamiamy rotację
    arrow.transform.forward = arrowSpawnPoint.forward;

    if (arrow.TryGetComponent(out Rigidbody rb))
    {
        rb.velocity = rb.angularVelocity = Vector3.zero;
        rb.AddForce(arrowSpawnPoint.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
    }

    // ► włączamy obracanie tylko na czas lotu
    var rot = arrow.GetComponent<ArrowRotation>();
    if (rot) rot.enabled = true;

    Debug.Log($"Wystrzelono strzałę ({strength:0.00})");
}

}
