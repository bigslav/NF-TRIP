using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Collider playerCollider;
    public LayerMask whoCanInteract;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            playerCollider = other;
            other.transform.parent = transform;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            other.transform.parent = null;
        }
    }
}
