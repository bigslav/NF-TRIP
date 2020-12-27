using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : Platform
{
    public float persistTime;
    public float disappearTime;
    public event EventHandler<PlatformFallEventArgs> PlatformFall;
    public PlatformFallingManager platformFallingManager;

    private void Start()
    {
        platformFallingManager.BeforePlatformFall += OnBeforePlatformFall;
    }

    private void OnBeforePlatformFall(object sender, EventArgs e)
    {
        if (playerCollider.transform.parent != null)
        {
            playerCollider.transform.parent = null;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        playerCollider = other;
        other.transform.parent = transform;
        PlatformFallEventArgs args = new PlatformFallEventArgs();
        args.persistTime = persistTime;
        args.disappearTime = disappearTime;
        args.platformGameObject = gameObject;
        PlatformFall(this, args);
    }

    public class PlatformFallEventArgs : EventArgs
    {
        public float persistTime { get; set; }
        public float disappearTime { get; set; }
        public GameObject platformGameObject { get; set; }
    }
}