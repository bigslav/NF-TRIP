using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeActivator : MonoBehaviour
{
    [SerializeField] private BridgeJointRotation _bridge;
    [SerializeField] private LayerMask _whoCanInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (_whoCanInteract == (_whoCanInteract | (1 << other.gameObject.layer)))
        {
            if (!_bridge._beingLifted)
            {
                _bridge._beingLifted = true;
                StartCoroutine(_bridge.Activate());
            }
        }
    }
}
