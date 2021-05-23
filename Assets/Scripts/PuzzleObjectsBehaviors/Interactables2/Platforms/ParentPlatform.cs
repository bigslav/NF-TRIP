using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlatform : MonoBehaviour
{
    public bool active = false;
    public bool activeAtStart = false;
    public bool oneWay = false;
    public bool oneWaySwitch = false;
    public bool onlyOneInteraction = false;
    public bool waitForNextInput = false;
    public bool parentingDisabled = false;
    public bool onlyOneInteractionUsed = false;

    private void BringDown() { }

    private void BringUp() { }

    public void MovePlatform() { }

    private void JustRotate() { }

    private void UpdateTarget() { }

    public void NextPlatform() { }

    public void Activate()
    {
        if (onlyOneInteraction)
        {
            if (!onlyOneInteractionUsed)
            {
                active = true;
                onlyOneInteractionUsed = true;
            }
        }
        else
        {
            active = true;
        }
    }

    public void Deactivate()
    {
        if (onlyOneInteraction)
        {
            if (!onlyOneInteractionUsed)
            {
                active = false;
                onlyOneInteractionUsed = true;
            }
        }
        else
        {
            active = false;
        }
    }

    public void Switch()
    {
        if (onlyOneInteraction)
        {
            if (!onlyOneInteractionUsed)
            {
                active = !active;
                onlyOneInteractionUsed = true;
            }
        }
        else
        {
            active = !active;
        }
    }

}
