using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLever : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private PlatformMoving movingPlatform;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerStay(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)) && Input.GetKeyDown(KeyCode.E))
        {
            movingPlatform.NextPlatform();
        }
    }
}
