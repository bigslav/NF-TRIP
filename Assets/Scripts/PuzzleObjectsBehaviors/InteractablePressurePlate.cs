using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePressurePlate : MonoBehaviour
{
    [SerializeField] private Interactable[] targetGameObject;
    [SerializeField] private LayerMask whoCanInteract;
    [SerializeField] private bool stayOnToUse;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (!stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Switch();
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Activate();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other);
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Deactivate();
            }
    }
}