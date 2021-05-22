using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchActivator : MonoBehaviour
{
    [SerializeField] private ParentPlatform[] targetGameObject;
    [SerializeField] private LayerMask whoCanInteract;
    [SerializeField] private bool stayOnToUse;
    [SerializeField] private bool reverseBehavior = false;

    //private bool soundPlayed = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!stayOnToUse && !reverseBehavior)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                playSound();
                playSoundPlatforms();
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Switch();
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Activate();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (reverseBehavior)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Switch();
            }

        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Deactivate();
            }
    }

    private void playSound()
    {
        FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/magic_button");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
        button.start();
        button.release();
    }
    private void playSoundPlatforms()
    {
        FMOD.Studio.EventInstance playSoundPlatforms = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/in_out_platforms");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playSoundPlatforms, transform, GetComponent<Rigidbody>());
        playSoundPlatforms.start();
        playSoundPlatforms.release();
    }
}