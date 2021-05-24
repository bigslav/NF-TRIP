using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSnapshot : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";
    [FMODUnity.EventRef]
    public string InSnapshot = "";

    private bool dialogueTriggered = false;
    private bool dialogueDone = false;

    FMOD.Studio.EventInstance InSnapshotEvent;
    FMOD.Studio.EventInstance DialogueEvent;
    FMOD.Studio.PLAYBACK_STATE state;

    // Start is called before the first frame update
    void Start()
    {
        InSnapshotEvent = FMODUnity.RuntimeManager.CreateInstance(InSnapshot);
        DialogueEvent = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueDone && dialogueTriggered)
        {
            DialogueEvent.getPlaybackState(out state);
            //Debug.Log("state: " + state);
            if (state.ToString() == "STOPPED")
            {
                InSnapshotEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                InSnapshotEvent.release();
                dialogueDone = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
        {
            if (!dialogueTriggered)
            {
                //Debug.Log("F_MaterialValue: " + F_MaterialValue);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(DialogueEvent, transform, GetComponent<Rigidbody>());
                DialogueEvent.start();
                InSnapshotEvent.start();
                DialogueEvent.release();
                dialogueTriggered = true;
            }
        }
    }
