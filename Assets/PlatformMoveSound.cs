using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveSound : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 oldpos;
    private FMOD.Studio.EventInstance PlatformSound;
    private bool soundPlayed = false;
    private float soundWaiting = 0f;

    void Start()
    {
        PlatformSound = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/in_out_platforms");
        pos = transform.position;
        oldpos = transform.position;
    }


    void FixedUpdate()
    {
        PlatformSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));

        pos = transform.position;

        soundWaiting -= Time.deltaTime;
        Debug.Log("soundWaiting: " + soundWaiting);

        if (soundWaiting <= 0)
        {
            soundPlayed = false;
        }

        if (Mathf.Abs(Mathf.Abs(pos.z) - Mathf.Abs(oldpos.z)) > 0.1f && !soundPlayed && soundWaiting <= 0)
        {
            PlatformSound.start();
            soundPlayed = true;
            soundWaiting = 1f;
        }

        oldpos = pos;
    }

    void OnDestroy()
    {
        PlatformSound.release();
    }
}
