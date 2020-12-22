using UnityEngine;

public class Puller : MonoBehaviour
{
    public PlayerMovement pullerMovement;
    public float pullSpeed;
    public float pullDirection;
    private GameObject objectToPull;
    private Rigidbody objectToPullRigidbody;

    private void Awake()
    {
        objectToPull = null;
        objectToPullRigidbody = null;
    }
    private void Update()
    {
        pullDirection = pullerMovement.moveDirection;
        pullSpeed = pullerMovement.runSpeed;

        if (objectToPull != null && Input.GetKey(KeyCode.F))
        {
            pullerMovement.isPulling = true;
            objectToPullRigidbody.velocity = new Vector3(pullSpeed * pullDirection, objectToPullRigidbody.velocity.y, objectToPullRigidbody.velocity.z);
        }
        else 
        {
            pullerMovement.isPulling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Box")
        {
            objectToPull = other.gameObject;
            objectToPullRigidbody = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Box")
        {
            objectToPull = null;
            objectToPullRigidbody = null;
        }
    }
}
