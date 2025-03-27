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
    public static bool wasDestroyed = false;

    public ParticleSystem particle;
    public AudioSource audioSource;

    void Awake()
    {
        wasDestroyed = false;
    }

    void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, initialHeight, startPosition.z);

        // Wyzeruj wynik na starcie (opcjonalnie)
        FruitScoreManager.Instance.AddPoints(0);
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
            if (newY >= maxHeight) movingUp = false;
        }
        else
        {
            newY -= Time.deltaTime * speed;
            if (newY <= minHeight) movingUp = true;
        }

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public void StopMoving() => stopMoving = true;
    public void ResumeMoving() => stopMoving = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Katana"))
        {
            HandleCut();
        }
    }

    public void OnCutBySword()
    {
        HandleCut();
    }

    private void HandleCut()
    {
        if (!wasDestroyed)
        {
            Debug.Log("StarterBanana przecięty!");

            wasDestroyed = true;

            if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
            if (TryGetComponent(out Collider collider)) collider.enabled = false;

            if (particle != null) particle.Play();
            if (audioSource != null) audioSource.Play();

            FruitScoreManager.Instance.AddPoints(0); // lub 10, jeśli chcesz

            Destroy(gameObject, 2f);
        }
    }
}
