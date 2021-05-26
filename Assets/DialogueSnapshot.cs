using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSnapshot : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PlayerStateEvent = "";
    [FMODUnity.EventRef]
    public string InSnapshot = "";

    public bool dialogueTriggered = false;
    public bool dialogueDone = false;

    public bool onlyMushroom = false;
    public bool onlyGolem = false;

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
            if (onlyMushroom || onlyGolem)
            {
                if (other.CompareTag("Mushroom") && onlyMushroom)
                {
                    playDialogue();
                }
                if (other.CompareTag("Golem") && onlyGolem)
                {
                    playDialogue();
                }
        }
    }

    void playDialogue()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(DialogueEvent, transform, GetComponent<Rigidbody>());
        DialogueEvent.start();
        InSnapshotEvent.start();
        DialogueEvent.release();
        dialogueTriggered = true;
    }
    }
}
