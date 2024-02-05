using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject trail; // Referencja do obiektu Trail Renderer

    void Start()
    {
        trail.SetActive(false); // Na początku smuga jest wyłączona
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Załóżmy, że wymach mieczem następuje po kliknięciu lewym przyciskiem myszy
        {
            SwingSword();
        }
    }

    void SwingSword()
    {
        // Tutaj możesz dodać dźwięk efektu wymachu mieczem, jeśli chcesz

        trail.SetActive(true); // Włączanie smugi

        // Resetowanie smugi po zakończeniu czasu trwania
        Invoke("DisableTrail", trail.GetComponent<TrailRenderer>().time);
    }

    void DisableTrail()
    {
        trail.SetActive(false); // Wyłączanie smugi po zakończeniu czasu trwania
    }
}
