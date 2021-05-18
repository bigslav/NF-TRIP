using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnRotation : MonoBehaviour
{
    public Vector3 rotation;

    private void Start()
    {
        if (transform.eulerAngles == rotation)
        {
            gameObject.layer = 10;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

    private void Update()
    {
        //Debug.Log(transform.eulerAngles + " | " + rotation);
        if (Vector3.SqrMagnitude(transform.eulerAngles - rotation) < 0.0001f)
        {
            gameObject.layer = 10;
            Debug.Log("Layer Upd");
        }
        else
        {
            gameObject.layer = 0;
        }
    }

}
