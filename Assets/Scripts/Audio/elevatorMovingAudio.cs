using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorMovingAudio : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 oldpos;
    private FMOD.Studio.EventInstance PlatformLoop;
    private FMOD.Studio.PLAYBACK_STATE PbState;

    void Start()
    {
        PlatformLoop = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/move_crate");
        pos = transform.position;
        oldpos = transform.position;
    }

    void FixedUpdate()
    {
        PlatformLoop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        PlatformLoop.getPlaybackState(out PbState);

        pos = transform.position;


        if (Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(oldpos.x)) > 0.01)
        {
            if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                PlatformLoop.start();
                //Debug.Log("MOVING");
            }
        }
        else if (Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(oldpos.x)) < 0.01 && PbState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            PlatformLoop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //Debug.Log("NOPE MOVING");
        }

        //Debug.Log(transform.name + " - oldpos: " + oldpos + "; pos: " + pos + "; PbState: " + PbState);
        oldpos = pos;
    }

    void OnDestroy()
    {
        PlatformLoop.release();
    }
}
