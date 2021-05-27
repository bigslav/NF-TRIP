using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineAndSwapTrigger : MonoBehaviour
{
    public CharacterSwitch characterSwitch;

    public bool combineOn;
    public bool swapControlOn;

    private void OnTriggerEnter(Collider other)
    {
        characterSwitch.combineOn = combineOn;
        characterSwitch.switchControlOn = swapControlOn;
        //PlayCageLanding();
    }

    void PlayCageLanding()
    {
        FMOD.Studio.EventInstance cageLanding = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/steel_cage");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(cageLanding, transform, GetComponent<Rigidbody>());
        cageLanding.start();
        cageLanding.release();
    }

}
