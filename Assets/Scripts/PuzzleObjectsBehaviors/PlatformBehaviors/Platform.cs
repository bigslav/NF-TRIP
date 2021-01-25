using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public static Collider playerCollider;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            playerCollider = other;
            other.transform.parent = transform;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            other.transform.parent = null;
        }
    }
}
