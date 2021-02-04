using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingTrigger : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlatformMoving movingPlatform;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            movingPlatform.NextPlatform();
        }
    }
}
