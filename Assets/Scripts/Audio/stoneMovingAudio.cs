using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneMovingAudio : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 oldpos;
    private FMOD.Studio.EventInstance PlatformLoop;
    private FMOD.Studio.PLAYBACK_STATE PbState;

    void Start()
    {
        PlatformLoop = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/movingStone");
        pos = transform.position;
        oldpos = transform.position;
    }

    void FixedUpdate()
    {
        PlatformLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        PlatformLoop.getPlaybackState(out PbState);

        pos = transform.position;

        if (pos != oldpos)
        {
            if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                PlatformLoop.start();
        }
        else if (pos == oldpos && PbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            PlatformLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        oldpos = pos;
    }

    void OnDestroy()
    {
        PlatformLoop.release();
    }
}
