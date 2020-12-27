using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappearAfterJump : Platform
{
    override public void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
        gameObject.SetActive(false);
    }
}
