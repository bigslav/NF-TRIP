using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeJointRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public float force = 5f;
    public float upTime = 10f;

    private FMOD.Studio.EventInstance bridgeEvent;
    private FMOD.Studio.PLAYBACK_STATE bridgeState;

    [HideInInspector]
    public bool _beingLifted = false;

    private void Start()
    {
        bridgeEvent = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/stoneBridge");
        bridgeEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody2D>()));
        bridgeEvent.getPlaybackState(out bridgeState);
    }


    private void Update()
    {
        if (_beingLifted)
        {
            _rb.AddForce(transform.up * force, ForceMode.Acceleration);
        }
        else 
        {
            _rb.AddForce(-transform.up * force, ForceMode.Acceleration);
        }
    }

    public IEnumerator Activate()
    {
        _beingLifted = true;
        bridgeEvent.start();
        yield return new WaitForSeconds(upTime);
        bridgeEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        bridgeEvent.start();
        _beingLifted = false;
        yield return new WaitForSeconds(5f);
        bridgeEvent.release();
    }
}
