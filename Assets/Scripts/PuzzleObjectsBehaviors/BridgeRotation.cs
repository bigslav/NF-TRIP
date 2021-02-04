using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private bool active = false;

    private void Update()
    {
        Debug.Log(transform.eulerAngles.z);
        if (active)
        {
            BringDown();
        }
        else 
        {
            BringUp();
        }
    }

    private void BringDown()
    {
        transform.Rotate(Vector3.forward * (-rotationSpeed * Time.deltaTime));

        if (transform.eulerAngles.z < 270)
        {
            rotationSpeed = 0;
        }
    }
    private void BringUp()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));

        if (transform.eulerAngles.z > 350)
        {
            rotationSpeed = 0;
        }
    }

    public void Acivate()
    {
        active = true;
        if (transform.eulerAngles.z < 270)
        {
            rotationSpeed = 0;
        }
        else 
        {
            rotationSpeed = 20;
        }
    }

    public void Deactivate()
    {
        active = false;
    }
}
