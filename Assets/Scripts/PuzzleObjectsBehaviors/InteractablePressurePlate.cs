using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePressurePlate : MonoBehaviour
{
    [SerializeField] private Interactable[] targetGameObject;
    [SerializeField] private LayerMask whoCanInteract;
    [SerializeField] private bool stayOnToUse;
    [SerializeField] private bool craneType = false;
    [SerializeField] private int presetPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!craneType) {
            if (!stayOnToUse)
                if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
                {
                    for (int i = 0; i < targetGameObject.Length; ++i)
                        targetGameObject[i].Switch();
                }
        }
        else
        {
            other.GetComponent<InputHandler>().preset = presetPoint;
            other.GetComponent<InputHandler>().isUsingMechanism = true;
            other.GetComponent<InputHandler>().mechanismUnderControl = targetGameObject[0]; // Control the first platform in the list.
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!craneType) { 
            if (stayOnToUse)
                if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
                {
                    for (int i = 0; i < targetGameObject.Length; ++i)
                        targetGameObject[i].Activate();
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!craneType) { 
            if (stayOnToUse)
                if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
                {
                    for (int i = 0; i < targetGameObject.Length; ++i)
                        targetGameObject[i].Deactivate();
                }
        }
        else
        {
            other.GetComponent<InputHandler>().preset = -1;
            other.GetComponent<InputHandler>().isUsingMechanism = false;
            other.GetComponent<InputHandler>().mechanismUnderControl = null;
        }
    }
}