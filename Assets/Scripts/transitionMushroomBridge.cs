using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class transitionMushroomBridge : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mushVCam = null;

    private void MakeTransition()
    { 
    
    }

    private void OnTriggerStay(Collider other)
    {
        MakeTransition();
    }
}
