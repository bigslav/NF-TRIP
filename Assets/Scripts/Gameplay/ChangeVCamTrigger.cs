using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeVCamTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera VCam;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerStay(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            if (other.gameObject.GetComponent<Character>().isActive)
            {
                VCam.Priority = 2;
            }
            else
            {
                VCam.Priority = 1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)) && other.gameObject.GetComponent<Character>().isActive)
        {
            VCam.Priority = 0;
        }
    }
}
