using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterBanana : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private float maxHeight = 1.5f;
    [SerializeField] private float minHeight = 1.3f;
    [SerializeField] private float initialHeight = 1.4f;
    [SerializeField] private float speed = 0.1f;

    private bool movingUp = true;
    private bool stopMoving = false;

    public event System.Action OnCollision;

    void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, initialHeight, startPosition.z);
    }

    void Update()
    {
        if (!stopMoving)
        {
            MoveUpDown();
        }

        transform.Rotate(Vector3.forward * Time.deltaTime * 20);
    }

    private void MoveUpDown()
    {
        float newY = transform.position.y;

        if (movingUp)
        {
            newY += Time.deltaTime * speed;
            if (newY >= maxHeight)
            {
                newY = maxHeight;
                movingUp = false;
                OnCollision?.Invoke();
            }
        }
        else
        {
            newY -= Time.deltaTime * speed;
            if (newY <= minHeight)
            {
                newY = minHeight;
                movingUp = true;
            }
        }

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public void StopMoving()
    {
        stopMoving = true;
    }

    public void ResumeMoving()
    {
        stopMoving = false;
    }

    // NOWA FUNKCJA – reakcja na cięcie
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Katana"))
        {
            Debug.Log("Banan przecięty!");

            // Wyłącz render i kolizję
            MeshRenderer mesh = GetComponent<MeshRenderer>();
            Collider collider = GetComponent<Collider>();
            if (mesh != null) mesh.enabled = false;
            if (collider != null) collider.enabled = false;

            // Cząsteczki (jeśli są childem)
            ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
            if (particle != null)
            {
                particle.Play();
            }

            // Dźwięk
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            Destroy(gameObject, 2f);
        }
    }
}
