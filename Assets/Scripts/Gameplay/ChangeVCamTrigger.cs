using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeVCamTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera previousVCam;
    public CinemachineVirtualCamera nextVCam;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerEnter(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            previousVCam.Priority = 0;
            nextVCam.Priority = 1;
        }
    }
}
