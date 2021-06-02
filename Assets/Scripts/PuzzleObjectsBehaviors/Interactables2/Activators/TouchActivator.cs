using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchActivator : MonoBehaviour
{
    [SerializeField] private ParentPlatform[] targetGameObject;
    [SerializeField] private LayerMask whoCanInteract;
    [SerializeField] private bool stayOnToUse;
    [SerializeField] private bool reverseBehavior = false;
    [SerializeField] private bool bridgeSound = false;

    public bool sinkStoneSound = false;
    public bool boatSound = false;
    public bool playOnce = false;
    private bool soundPlayedOnce = false;
    public bool needBothCharacters = false;
    public Character character;
    private int charactersCount;
    //private bool soundPlayed = false;

    private string PlayerStateEvent = "event:/dialogue/cave/MG8";
    private string InSnapshot = "snapshot:/Dialogue";
    public bool dialogueTriggered = false;
    public bool dialogueDone = false;
    FMOD.Studio.EventInstance InSnapshotEvent;
    FMOD.Studio.EventInstance DialogueEvent;
    FMOD.Studio.PLAYBACK_STATE state;
    public bool longDialogue = false;

    private void Start()
    {
        InSnapshotEvent = FMODUnity.RuntimeManager.CreateInstance(InSnapshot);
        DialogueEvent = FMODUnity.RuntimeManager.CreateInstance(PlayerStateEvent);
    }

    private void Update()
    {
        if (Time.timeScale != 1)
        {
            DialogueEvent.setPaused(true);
        }
        else if (Time.timeScale == 1)
        {
            DialogueEvent.setPaused(false);
        }
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
        if (!stayOnToUse && !reverseBehavior)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                charactersCount++;
                if (needBothCharacters)
                {
                    if (character.isCombined || charactersCount == 2) 
                    {
                        if (sinkStoneSound || boatSound)
                        {
                            if (!dialogueTriggered && longDialogue)
                            {
                                playDialogue();
                            }
                        }
                        else
                        {
                            if (!soundPlayedOnce)
                            {
                                playSound();
                                playSoundPlatforms();
                                if (playOnce)
                                {
                                    soundPlayedOnce = true;
                                }
                            }
                        }
                        for (int i = 0; i < targetGameObject.Length; ++i)
                            targetGameObject[i].Switch();
                    }
                }
                else
                {
                    if (sinkStoneSound || boatSound)
                    {

                    }
                    else
                    {
                        if (!soundPlayedOnce)
                        {
                            playSound();
                            playSoundPlatforms();
                            if (playOnce)
                            {
                                soundPlayedOnce = true;
                            }
                        }
                    }
                    for (int i = 0; i < targetGameObject.Length; ++i)
                        targetGameObject[i].Switch();
                }
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Activate();
            }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (longDialogue && dialogueTriggered && other.CompareTag("Golem"))
        //{
        //    StopDialogue();
        //    longDialogue = false;
        //}

        if (reverseBehavior)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                charactersCount--;
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Switch();
            }

        if (stayOnToUse)
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                charactersCount--;
                for (int i = 0; i < targetGameObject.Length; ++i)
                    targetGameObject[i].Deactivate();
            }

        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            charactersCount--;
        }
    }

    private void playSound()
    {
        FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/magic_button");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
        button.start();
        button.release();
    }
    private void playSoundPlatforms()
    {
        FMOD.Studio.EventInstance playSoundPlatforms;
        if (bridgeSound)
        {
            playSoundPlatforms = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/bridge");
        }
        else
        {
            playSoundPlatforms = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/in_out_platforms");
        }
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playSoundPlatforms, transform, GetComponent<Rigidbody>());
        playSoundPlatforms.start();
        playSoundPlatforms.release();
    }
    void playDialogue()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(DialogueEvent, transform, GetComponent<Rigidbody>());
        DialogueEvent.start();
        InSnapshotEvent.start();
        dialogueTriggered = true;
    }

    public void StopDialogue()
    {
        Debug.Log("stopdialogue");
        DialogueEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        DialogueEvent.release();
    }
}