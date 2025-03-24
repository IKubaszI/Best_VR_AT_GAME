using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class SwordCut : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VectorFruit velocityEstimator;
    public LayerMask sliceableLayer;
    public Material crossSectionMaterial;
    public float cutForce = 200f;

    AudioSource audioData;

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;

            // ➕ sprawdzamy czy to StartingBanana
            if (target.CompareTag("StartingBanana"))
            {
                StarterBanana starter = target.GetComponent<StarterBanana>();
                if (starter != null)
                {
                    Debug.Log("StarterBanana przecięty przez miecz!");
                    // symulujemy uderzenie
                    starter.OnCutBySword();
                    return;
                }
            }

            // Inne banany – slice normalnie
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        MeshRenderer mesh = target.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = target.GetComponent<BoxCollider>();
        MeshCollider meshCollider = target.GetComponent<MeshCollider>();
        Transform animation = target.transform.childCount > 0 ? target.transform.GetChild(0) : null;

        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            Debug.Log("Slice wykonany!");

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);

            SetupSlicedComponent(upperHull);
            SetupSlicedComponent(lowerHull);

            audioData = target.GetComponent<AudioSource>();
            if (audioData != null) audioData.Play();

            // Wyłączenie oryginalnego obiektu
            if (mesh != null) mesh.enabled = false;
            if (boxCollider != null) boxCollider.enabled = false;
            if (meshCollider != null) meshCollider.enabled = false;
            if (animation != null) animation.gameObject.SetActive(true);

            // Poinformuj FlyingBanana (jeśli ma)
            FlyingBanana fb = target.GetComponent<FlyingBanana>();
            if (fb != null)
            {
                fb.OnSliced();
            }

            Destroy(target, 3f);
        }
        else
        {
            Debug.Log("Slice NIE wykonany.");
        }
    }

    public void Explode(GameObject target)
    {
        MeshRenderer mesh = target.transform.GetChild(1).GetComponent<MeshRenderer>();
        BoxCollider collider = target.GetComponent<BoxCollider>();
        MeshCollider meshCollider = target.GetComponent<MeshCollider>();
        Transform animation = target.transform.GetChild(0);

        mesh.enabled = false;
        collider.enabled = false;
        meshCollider.enabled = false;
        animation.gameObject.SetActive(true);

        audioData = target.GetComponent<AudioSource>();
        if (audioData != null) audioData.Play();

        Destroy(target, 3f);
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

        slicedObject.layer = LayerMask.NameToLayer("Sliceable");
        slicedObject.tag = "Fruit";

        var fb = slicedObject.AddComponent<FlyingBanana>();
        fb.disablePoints = true;

        rb.useGravity = true;
        rb.mass = 0.3f;
        rb.drag = 1f;
        rb.angularDrag = 2f;
        rb.velocity = Vector3.up * 2f + Random.insideUnitSphere * 0.5f;

        Destroy(slicedObject, 2f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bomb"))
        {
            Explode(collision.gameObject);
        }
    }
    
}
