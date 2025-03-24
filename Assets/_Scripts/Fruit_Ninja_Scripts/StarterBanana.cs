using UnityEngine;
using TMPro;

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

    // Dodatkowe pola
    public TMP_Text scoreText;
    private int scoreShooter = 0;
    public ParticleSystem particle;
    public AudioSource audioSource;

    void Awake()
    {
        wasDestroyed = false;  // Reset flagi przy starcie gry
    }

    void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, initialHeight, startPosition.z);
        UpdateScoreText();
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
                movingUp = false;
        }
        else
        {
            newY -= Time.deltaTime * speed;
            if (newY <= minHeight)
                movingUp = true;
        }

        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public void StopMoving() => stopMoving = true;
    public void ResumeMoving() => stopMoving = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Katana"))
        {
            Debug.Log("Banan przecięty!");

            // Wyłącz render i kolizję
            if (TryGetComponent(out MeshRenderer mesh)) mesh.enabled = false;
            if (TryGetComponent(out Collider collider)) collider.enabled = false;

            // Cząsteczki
            if (particle != null) particle.Play();

            // Dźwięk
            if (audioSource != null) audioSource.Play();

            // Punkty
            AddPoints(0);

            // Flaga informująca spawner
            wasDestroyed = true;

            Destroy(gameObject, 2f);
        }
    }

    void AddPoints(int points)
    {
        scoreShooter += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Twoj wynik: " + scoreShooter;
    }
    public void OnCutBySword()
{
    if (!wasDestroyed)
    {
        Debug.Log("OnCutBySword: StarterBanana przecięty przez miecz!");

        wasDestroyed = true;

        // Efekty
        if (particle != null) particle.Play();
        if (audioSource != null) audioSource.Play();

        AddPoints(0); // Dodaj punkty (lub zostaw 0)

        // Znikanie obiektu
        Destroy(gameObject, 2f);
    }
}

}
