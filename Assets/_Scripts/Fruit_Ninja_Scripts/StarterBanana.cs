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
    private bool rotation = true;
    private bool stopMoving = false;

    

    public event System.Action OnCollision;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        transform.position = new Vector3(startPosition.x, initialHeight, startPosition.z);
    }

    // Update is called once per frame
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

    bool aCollison = false;

    public void OnCollisionEnter(Collision collision)
    { 
        bool aCollision = true;
        Debug.Log("Col: " + aCollision);
        StartGame startGame = FindObjectOfType<StartGame>();
        if (startGame != null)
        {
            startGame.start();
        }
    }


}
