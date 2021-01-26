using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLever : MonoBehaviour
{
    public PlatformMoving movingPlatform;

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.layer == 8 || other.gameObject.layer == 9) && Input.GetKeyDown(KeyCode.E))
        {
            movingPlatform.NextPlatform();
        }
    }
}
