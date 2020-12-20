using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour
{
    [SerializeField]
    public PlayerMovement playerMovement;
    private GameObject objectToPull;
    public float pullSpeed;
    public float pullDirection;

    private void Awake()
    {
        objectToPull = null;
    }
    private void Update()
    {
        pullDirection = playerMovement.moveDirection;
        pullSpeed = playerMovement.runSpeed;

        if (objectToPull != null && Input.GetKey(KeyCode.F))
        {
            Debug.Log("Pulling");
            playerMovement.isPulling = true;
            objectToPull.transform.Translate(Time.deltaTime * pullSpeed * new Vector3(pullDirection, 0));
        }
        else 
        {
            Debug.Log("Not Pulling");
            playerMovement.isPulling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Box")
        {
            objectToPull = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Box")
        {
            objectToPull = null;
        }
    }
}
