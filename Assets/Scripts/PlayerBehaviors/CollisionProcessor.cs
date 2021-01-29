using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;

    public bool isGrounded;
    public bool isOnTopOfGolem;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isGrounded = true;
        }

        if (collision.gameObject.layer == 11)
        {
            isOnTopOfGolem = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isGrounded = true;
        }

        if (collision.gameObject.layer == 11)
        {
            isOnTopOfGolem = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {
            isGrounded = false;
        }

        if (collision.gameObject.layer == 11)
        {
            isOnTopOfGolem = false;
        }
    }
}