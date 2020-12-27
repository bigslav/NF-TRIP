using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [HideInInspector]
    public static Collider playerCollider;

    public virtual void OnTriggerEnter(Collider other)
    {
        playerCollider = other;
        other.transform.parent = transform;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
