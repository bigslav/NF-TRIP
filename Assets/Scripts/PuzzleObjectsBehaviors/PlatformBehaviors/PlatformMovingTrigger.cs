using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovingTrigger : MonoBehaviour
{
    public PlatformMoving movingPlatform;

    private void OnTriggerEnter(Collider other)
    {
        movingPlatform.NextPlatform();
    }
}
