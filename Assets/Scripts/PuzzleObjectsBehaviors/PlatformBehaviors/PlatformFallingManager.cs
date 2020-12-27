using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallingManager : ObjectActivator
{
    public PlatformFalling[] fallingPlatforms;
    public event EventHandler BeforePlatformFall;

    private void Start()
    {
        foreach (PlatformFalling platform in fallingPlatforms)
        {
            platform.PlatformFall += OnPlatformFall;
        }
    }

    private void OnPlatformFall(object sender, PlatformFalling.PlatformFallEventArgs e)
    {
        StartCoroutine(DisappearAndReapear(e.platformGameObject, e.persistTime, e.disappearTime));
    }

    IEnumerator DisappearAndReapear(GameObject platform, float persistTime, float disappearTime) 
    {
        yield return new WaitForSeconds(persistTime);
        BeforePlatformFall(this, EventArgs.Empty);
        Deactivate(platform);
        yield return new WaitForSeconds(disappearTime);
        Activate(platform);
    }
}
