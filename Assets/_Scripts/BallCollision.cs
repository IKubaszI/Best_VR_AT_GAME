using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public AudioClip pinHitSound;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            AudioSource.PlayClipAtPoint(pinHitSound, collision.contacts[0].point);
        }
    }
}
