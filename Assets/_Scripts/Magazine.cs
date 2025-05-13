using UnityEngine;

public class Magazine : MonoBehaviour
{
    [Header("Ilość pocisków w tym magazynku")]
    public int numberOfBullets = 8;

    [Header("Prefab pustego magazynka")]
    public GameObject emptyMagazinePrefab;

    // Flaga, żeby jednorazowo podmienić
    private bool _swapped = false;

    void Update()
    {
        // Gdy liczba pocisków spadnie do zera i jeszcze nie podmieniono
        if (!_swapped && numberOfBullets <= 0)
            SwapToEmpty();
    }

    private void SwapToEmpty()
    {
        _swapped = true;

        if (emptyMagazinePrefab == null)
        {
            Debug.LogWarning($"[{name}] nie przypięto emptyMagazinePrefab!", this);
            return;
        }

        // Tworzymy nowy (pusty) magazynek w tej samej pozycji i hierarchii
        Instantiate(
            emptyMagazinePrefab,
            transform.position,
            transform.rotation,
            transform.parent
        );

        // Usuwamy ten (pełny) magazynek
        Destroy(gameObject);
    }
}
