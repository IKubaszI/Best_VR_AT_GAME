using UnityEngine;
public class target_strzelnica : MonoBehaviour
{
    public DisappearManager disappearManager;

    void Start()
    {
        disappearManager = transform.parent.GetComponent<DisappearManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Trafiono w tarczę!");
            disappearManager.DisappearAndMove();
        }
    }
}