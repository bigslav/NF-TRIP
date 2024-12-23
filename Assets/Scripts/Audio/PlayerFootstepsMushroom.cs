﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsMushroom : MonoBehaviour
{

    [Header("FMOD Settings")]
    //[SerializeField] [FMODUnity.EventRef] private string FootstepsEventPath;
    //[SerializeField] [FMODUnity.EventRef] private string JumpAndLandPath;
    private string MaterialParameterName = "Terrain";
    //private string JumpOrLand = "Jump Or Land";
    private bool Grounded;
    //private bool GroundedPlayed;

    [Header("Playback Settings")]
    //[SerializeField] private float StepDistance = 2.0f;
    private float RayDistance = 1.3f;
    public string[] MaterialTypes;
    [HideInInspector] public int DefulatMaterialValue = 0;

    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;

    Animator anim;
    void Start()
    {
        //GroundedPlayed = false;
        anim = GetComponent<Animator>();
        AddEvent(2, 0.1f, "Step", 0); // WalkG
        //AddEvent(2, 0.10f, "Step", 0); // WalkG
        //AddEvent(2, 0.5f, "Step", 0); // The second step of the animation
        //AddEvent(2, 0.5f, "Step", 0); // WalkG
        //AddEvent(2, 0.21f, "Step", 0); // WalkG
        //AddEvent(2, 0.27f, "Step", 0);
        //AddEvent(1, 0.00f, "Jump", 0);
    }

    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        anim = GetComponent<Animator>();
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
        clip.AddEvent(animationEvent);
    }

    public void Step()
    {
        MaterialCheck();
        PlayFootstep();
    }

    void MaterialCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))
        {
            if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())
            {
                //Debug.Log("IN");
                F_MaterialValue = hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>().MaterialValue;
            }
            else
            {
                F_MaterialValue = DefulatMaterialValue;
            }
        }
        else
        {
            F_MaterialValue = DefulatMaterialValue;
        }
    }

    void PlayFootstep()
    {
        //Debug.Log("F_MaterialValue: " + F_MaterialValue);
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/char/mushroom/step");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }
}
