using UnityEngine;

public class PlatformDisappearAfterJump : Platform
{
    override public void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            other.transform.parent = null;
            gameObject.SetActive(false);
        }
    }
}
