using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgePressPlate : MonoBehaviour
{
    [SerializeField] private BridgeRotation bridge;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerStay(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            bridge.Acivate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            bridge.Deactivate();
        }
    }
}
