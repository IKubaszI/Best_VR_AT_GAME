using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingTargetUpDown : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float minHeight = 0.3f;
    [SerializeField] private float initialHeight = 1f;
    [SerializeField] private float speed = 1f;

    private bool movingUp = true;
    private bool stopMoving = false;

    public event System.Action OnCollision;

    private void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, initialHeight, startPosition.z);
    }

    private void Update()
    {
        if (!stopMoving)
        {
            MoveUpDown();
        }
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
}
