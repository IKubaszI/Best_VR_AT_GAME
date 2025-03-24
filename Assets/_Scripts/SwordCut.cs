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
    AudioSource audioData;
    public float cutForce = 2000;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;

            Slice(target);


        }
    }

    public void Slice(GameObject target)
    {
        MeshRenderer mesh = target.GetComponent<MeshRenderer>();
        BoxCollider collider = target.GetComponent<BoxCollider>();
        MeshCollider meshCollider = target.GetComponent<MeshCollider>();
        Transform animation = target.transform.GetChild(0);
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
            audioData = target.GetComponent<AudioSource>();
            audioData.Play(0);
            mesh.enabled = false;
            // disable collider and collider mesh
            collider.enabled = false;
            meshCollider.enabled = false;
            animation.gameObject.SetActive(true);
            Destroy(target, 3);
        }
    }

    public void Explode(GameObject target)
    {
        MeshRenderer mesh = target.transform.GetChild(1).gameObject.GetComponent<MeshRenderer>();
        //SphereCollider collider=target.GetComponent<SphereCollider>();
        BoxCollider collider = target.GetComponent<BoxCollider>();
        MeshCollider meshCollider = target.GetComponent<MeshCollider>();

        Transform animation = target.transform.GetChild(0);
        mesh.enabled = false;
        // disable collider and collider mesh
        collider.enabled = false;
        meshCollider.enabled = false;
        animation.gameObject.SetActive(true);
        audioData = target.GetComponent<AudioSource>();
        audioData.Play(0);
        Destroy(target, 3);
    }


    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
        Destroy(slicedObject, 2);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {

            Explode(collision.gameObject);
        }
    }

}
