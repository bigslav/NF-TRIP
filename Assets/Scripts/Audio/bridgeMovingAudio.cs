using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeMovingAudio : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 oldpos;
    private FMOD.Studio.EventInstance bridgeEvent;
    private FMOD.Studio.PLAYBACK_STATE PbState;

    void Start()
    {
        bridgeEvent = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/stoneBridge");
        pos = transform.position;
        oldpos = transform.position;
    }

    void FixedUpdate()
    {
        bridgeEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        bridgeEvent.getPlaybackState(out PbState);

        pos = transform.position;

        if (Mathf.Abs(Mathf.Abs(pos.y) - Mathf.Abs(oldpos.y)) > 0.015)
        {
            if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                bridgeEvent.start();
        }
        oldpos = pos;
    }

    void OnDestroy()
    {
        bridgeEvent.release();
    }
}
