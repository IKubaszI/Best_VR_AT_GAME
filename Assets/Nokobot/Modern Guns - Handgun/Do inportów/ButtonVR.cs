using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    [Header("References")]
    public GameObject button;         // obiekt wizualnego przycisku
    public UnityEvent onPress;        // zdarzenie wywoływane przy wciśnięciu
    public UnityEvent onRelease;      // zdarzenie wywoływane przy zwolnieniu

    GameObject presser;               // kto aktualnie naciska przycisk
    AudioSource sound;                // dźwięk przycisku
    bool isPressed;                   // czy przycisk jest wciśnięty

    void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            // przesunięcie przycisku "w dół"
            button.transform.localPosition = new Vector3(0f, 0.003f, 0f);

            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // zwolnij tylko jeśli to ten sam obiekt, który wcisnął przycisk
        if (other.gameObject == presser)
        {
            // przywrócenie przycisku "w górę"
            button.transform.localPosition = new Vector3(0f, 0.015f, 0f);

            onRelease.Invoke();
            isPressed = false;
        }
    }

    /// <summary>
    /// Przykładowa metoda tworząca kulę w scenie.
    /// </summary>
    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0f, 1f, 2f);
        sphere.AddComponent<Rigidbody>();
    }
}
