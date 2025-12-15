using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [Header("Platform Settings")]
    public float moveSpeed = 1.0f;
    public float minHeight = 0f;
    public float maxHeight = 5f;
    
    [Header("Reset Settings")]
    public Vector3 startPosition;
    
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        if (isMovingUp && transform.position.y < maxHeight)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else if (isMovingDown && transform.position.y > minHeight)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }
    
    // Metody dla Twoich przycisków ButtonVR
    public void StartMovingUp()
    {
        isMovingUp = true;
        Debug.Log("Platform moving up");
    }
    
    public void StopMovingUp()
    {
        isMovingUp = false;
    }
    
    public void StartMovingDown()
    {
        isMovingDown = true;
        Debug.Log("Platform moving down");
    }
    
    public void StopMovingDown()
    {
        isMovingDown = false;
    }
    
    public void ResetToStartPosition()
    {
        transform.position = startPosition;
        isMovingUp = false;
        isMovingDown = false;
    }
}
