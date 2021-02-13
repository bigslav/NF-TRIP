using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePressPlate : MonoBehaviour
{
    [SerializeField] private Interactable targetGameObject;
    [SerializeField] private LayerMask whoCanInteract;
    [SerializeField] private bool stayOnToUse; 

    private void OnTriggerEnter(Collider other)
    {
        if (!stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                targetGameObject.Switch();
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                targetGameObject.Acivate();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                targetGameObject.Deactivate();
            }
    }
}
