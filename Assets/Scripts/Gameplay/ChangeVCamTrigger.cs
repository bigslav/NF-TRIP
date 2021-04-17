using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeVCamTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera previousVCam;
    public CinemachineVirtualCamera nextVCam;

    private void OnTriggerEnter(Collider other)
    {
        previousVCam.Priority = 0;
        nextVCam.Priority = 1;
    }
}
