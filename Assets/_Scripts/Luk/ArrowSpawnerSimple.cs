using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawnerSimple : MonoBehaviour
{
    [Header("Prefab Strzały")]
    [SerializeField] private GameObject arrowPrefab;

    private void Start()
    {
        SpawnArrow();
    }

    public void SpawnArrow()
    {
        // Tworzymy nową niewidzialną strzałę jako dziecko
        GameObject arrow = Instantiate(arrowPrefab, transform);
        
        // Reset transformacji względem chest
        arrow.transform.localPosition = Vector3.zero;
        arrow.transform.localRotation = Quaternion.identity;

        // Zapewniamy, że jest niewidoczna i przygotowana
        var renderer = arrow.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = false;

        var grabInteractable = arrow.GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            Debug.LogError("Prefab Arrow musi mieć XRGrabInteractable");
            return;
        }

        // Dodajemy listener do grabowania
        grabInteractable.selectEntered.AddListener(OnArrowGrabbed);
    }

    private void OnArrowGrabbed(SelectEnterEventArgs args)
    {
        // Pobieramy chwyconą strzałę
        XRGrabInteractable grabbedArrow = (XRGrabInteractable)args.interactableObject;

        // Odłączamy od rodzica (opuszcza skrzynię)
        grabbedArrow.transform.SetParent(null, true);

        // Włączamy widoczność
        var renderer = grabbedArrow.GetComponentInChildren<MeshRenderer>();
        renderer.enabled = true;

        // Spawnujemy nową strzałę w skrzyni
        SpawnArrow();
    }
}
