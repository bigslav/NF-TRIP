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
    }

}
