using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance caveMusicEvent;
    private bool musicOneSound = false;
    // Start is called before the first frame update
    void Start()
    {
        caveMusicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/music/cave/main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golem") && !musicOneSound)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(caveMusicEvent, transform, GetComponent<Rigidbody>());
            caveMusicEvent.start();
            caveMusicEvent.release();
            musicOneSound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Golem") && !musicOneSound)
        {
            caveMusicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            caveMusicEvent.release();
            musicOneSound = false;
        }
    }
}
