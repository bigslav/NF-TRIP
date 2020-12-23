using UnityEngine;

public class Rider : MonoBehaviour
{
    public PlayerMovement riderMovement;
    public PlayerMovement mountMovement;
    public Rigidbody riderRigidbody;
    public float riderSeatHeight;

    void Start()
    {
        riderMovement.IsRiding = false;
    }

    private void Update()
    {
        if (riderMovement.IsRiding && !riderMovement.isActive)
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
        if (collision.gameObject.name == "PlayerBig")
        {
            riderMovement.IsRiding = true;
        }
        else
        {
            riderMovement.IsRiding = false;
        }
    }

}
