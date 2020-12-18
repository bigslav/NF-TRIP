using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : MonoBehaviour
{
    public bool isMounting;
    [SerializeField] 
    private PlayerMovement riderMovement;
    [SerializeField]
    private PlayerMovement mount;
    [SerializeField]
    private Rigidbody riderRigidbody;
    [SerializeField]
    private BoxCollider riderCollider;
    [SerializeField]
    private BoxCollider mountCollider;
    private float offset;

    void Start()
    {
        offset = 1.55f;
        isMounting = false;
    }

    private void FixedUpdate()
    {
        if (riderMovement.isGrounded == false) 
        {
            isMounting = false;
        }

        if (isMounting && !riderMovement.isActive)
        {
            riderRigidbody.isKinematic = true;
            transform.position = new Vector3(mount.transform.position.x, mount.transform.position.y + offset, mount.transform.position.z);
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
