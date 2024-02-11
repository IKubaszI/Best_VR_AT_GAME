using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTarget : MonoBehaviour, IHittablee
{
    private Rigidbody rb;
    private bool stopped = false;
    public int scoreOnHit = 0;
    private Vector3 nextPosition;
    private Vector3 originPosition;
    
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private float arriveThreshold, movementRadius = 2, speed = 1;
    
    [Header("Vertical Movement")]
    public float maxHeight = 3.0f;
    public float minHeight = 1.0f;
    public float verticalSpeed = 1.0f;
    private float currentHeight;
    private bool movingUp = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originPosition = transform.position;
        nextPosition = GetNewMovementPosition();
        currentHeight = Random.Range(minHeight, maxHeight);
    }

    private Vector3 GetNewMovementPosition()
    {
        return originPosition + (Vector3)Random.insideUnitCircle * movementRadius;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((rb.isKinematic || collision.gameObject.CompareTag("Arrow")) == false)
        {
            audioSource.Play();
        }
    }

    public void GetHit()
    {
        
        GetPoint getPointScript = FindObjectOfType<GetPoint>();
        if (getPointScript != null)
        {
            getPointScript.AddPoints(scoreOnHit);
        }
    }

    private void FixedUpdate()
    {
        if (stopped == false)
        {
            if (Vector3.Distance(transform.position, nextPosition) < arriveThreshold)
            {
                nextPosition = GetNewMovementPosition();
            }

            Vector3 direction = nextPosition - transform.position;
            rb.MovePosition(transform.position + direction.normalized * Time.fixedDeltaTime * speed);
            
            // Vertical movement
            if (movingUp)
            {
                currentHeight += verticalSpeed * Time.fixedDeltaTime;
                if (currentHeight >= maxHeight)
                {
                    currentHeight = maxHeight;
                    movingUp = false;
                }
            }
            else
            {
                currentHeight -= verticalSpeed * Time.fixedDeltaTime;
                if (currentHeight <= minHeight)
                {
                    currentHeight = minHeight;
                    movingUp = true;
                }
            }
            
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }
    }
}

public interface IHittablee
{
    void GetHit();
}
