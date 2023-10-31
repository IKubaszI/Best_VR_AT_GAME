using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTarget : MonoBehaviour
{
    [SerializeField] private MovingTargetUpDown movingTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("arrow"))
        {
            
            movingTarget?.StopMoving();
        }
    }
}
