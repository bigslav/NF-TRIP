using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{
    public bool isMounting;
    public PlayerMovement riderMovement;
    public PlayerMovement mount;
    public Rigidbody riderRigidbody;
    public float riderSeatHeight;

    void Start()
    {
        riderSeatHeight = 1.55f;
        isMounting = false;
    }

    private void FixedUpdate()
    {
        if (isMounting && !riderMovement.isActive)
        {
            riderRigidbody.isKinematic = true;
            transform.position = new Vector3(mount.transform.position.x, mount.transform.position.y + riderSeatHeight, mount.transform.position.z);
        }
        else {
            riderRigidbody.isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player_1")
        {
            isMounting = true;
        }
        else
        {
            isMounting = false;
        }
    }
}
