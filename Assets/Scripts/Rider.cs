using UnityEngine;

public class Rider : MonoBehaviour
{
    public PlayerMovement riderMovement;
    public PlayerMovement mountMovement;
    public Rigidbody riderRigidbody;
    public float riderSeatHeight;

    void Start()
    {
        riderSeatHeight = 1.55f;
        riderMovement.isRiding = false;
    }

    private void Update()
    {
        if (riderMovement.isRiding && !riderMovement.isActive)
        {
            riderRigidbody.isKinematic = true;
            transform.position = new Vector3(mountMovement.transform.position.x, mountMovement.transform.position.y + riderSeatHeight, mountMovement.transform.position.z);
        }
        else 
        {
            riderRigidbody.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player_1")
        {
            riderMovement.isRiding = true;
        }
        else
        {
            riderMovement.isRiding = false;
        }
    }

}
