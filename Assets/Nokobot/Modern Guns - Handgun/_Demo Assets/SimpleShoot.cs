using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location References")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Czas życia obiektów łusek i flasha")]
    [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Siła wystrzału pocisku")]
    [SerializeField] private float shotPower = 500f;
    [Tooltip("Siła wyrzutu łuski")]
    [SerializeField] private float ejectPower = 150f;

    [Header("Audio")]
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip noAmmoSound;

    [Header("Magazine & Interaction")]
    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    private bool hasSlide = true;

    void Start()
    {
        // Domyślne referencje
        if (barrelLocation == null)
            barrelLocation = transform;
        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        // Podpinamy eventy do podmieniania magazynka
        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void PullTheTrigger()
    {
        // Możemy strzelić tylko gdy jest magazynek, jest w nim >0 pocisków i suwadło jest założone
        if (magazine != null && magazine.numberOfBullets > 0 && hasSlide)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            source.PlayOneShot(noAmmoSound);
        }
    }

    // Wywoływane z eventu animacji ("Fire" → poniżej)
    void Shoot()
    {
        // Zmniejszamy licznik w magazynku
        magazine.numberOfBullets--;

        source.PlayOneShot(fireSound);

        // Muzzle flash
        if (muzzleFlashPrefab)
        {
            var flash = Instantiate(
                muzzleFlashPrefab,
                barrelLocation.position,
                barrelLocation.rotation
            );
            Destroy(flash, destroyTimer);
        }

        // Tworzymy pocisk
        if (bulletPrefab)
        {
            var bullet = Instantiate(
                bulletPrefab,
                barrelLocation.position,
                barrelLocation.rotation
            );
            bullet.GetComponent<Rigidbody>()
                  .AddForce(barrelLocation.forward * shotPower);
        }
    }

    // Wywoływane z eventu animacji ("Fire" → poniżej)
    void CasingRelease()
    {
        if (!casingExitLocation || !casingPrefab) return;

        var casing = Instantiate(
            casingPrefab,
            casingExitLocation.position,
            casingExitLocation.rotation
        );
        var rb = casing.GetComponent<Rigidbody>();
        rb.AddExplosionForce(
            Random.Range(ejectPower * 0.7f, ejectPower),
            casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f,
            1f
        );
        rb.AddTorque(
            new Vector3(
                0,
                Random.Range(100f, 500f),
                Random.Range(100f, 1000f)
            ),
            ForceMode.Impulse
        );
        Destroy(casing, destroyTimer);
    }

    // Wywoływane, gdy wrzucamy nowy magazynek do gniazda
    public void AddMagazine(XRBaseInteractable interactable)
    {
        magazine = interactable.GetComponent<Magazine>();
        hasSlide = false;
        source.PlayOneShot(reloadSound);
    }

    // Wywoływane, gdy wyjmujemy magazynek
    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        magazine = null;
        source.PlayOneShot(reloadSound);
    }

    // Wywoływane przy „przeładowaniu” suwadła
    public void Slide()
    {
        hasSlide = true;
        source.PlayOneShot(reloadSound);
    }
}
