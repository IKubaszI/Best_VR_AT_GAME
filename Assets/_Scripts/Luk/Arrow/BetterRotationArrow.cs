using UnityEngine;

public class BetterRotationArrow : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude > 0.1f)
            transform.forward = Vector3.Slerp(
                transform.forward,
                rb.velocity.normalized,
                Time.fixedDeltaTime * 15f);
    }
}
