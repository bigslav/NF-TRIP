using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsMushroom : MonoBehaviour
{

    [Header("FMOD Settings")]
    [SerializeField] [FMODUnity.EventRef] private string FootstepsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string JumpAndLandPath;
    private string MaterialParameterName = "Terrain";
    private string JumpOrLand = "Jump Or Land";
    private bool Grounded;
    private bool GroundedPlayed;

    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.3f;
    public string[] MaterialTypes;
    [HideInInspector] public int DefulatMaterialValue;

    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;

    Animator anim;
    void Start()
    {
        GroundedPlayed = false;
        anim = GetComponent<Animator>();
        AddEvent(2, 0.10f, "Step", 0); // WalkG
        AddEvent(2, 0.25f, "Step", 0);
        AddEvent(1, 0.00f, "Jump", 0);
    }

    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        anim = GetComponent<Animator>();
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
        Debug.Log("" + anim.runtimeAnimatorController.animationClips.Length);
        clip.AddEvent(animationEvent);
    }

    public void Step()
    {
        MaterialCheck();
        PlayFootstep();
    }

    public void Jump()
    {
        MaterialCheck();
        PlayJump();
    }
    public void Update()
    {
        //Debug.Log(Grounded + " : " + GroundedPlayed);
        //GroundedCheck();
        //if (Grounded == true && GroundedPlayed == false)
        //{
        //    MaterialCheck();
        //    PlayLand();
        //    GroundedPlayed = true;
        //}
        //else
        //{
        //    GroundedPlayed = false;
        //}
    }

    void MaterialCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))
        {
            if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())
            {
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
        Debug.Log("Footstep: " + F_MaterialValue);
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }

    void PlayJump()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(JumpAndLandPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }
    //void PlayLand()
    //{
    //    FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(JumpAndLandPath);
    //    FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
    //    Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
    //    Footstep.setParameterByName(JumpOrLand, 1.0f);
    //    Footstep.start();
    //    Footstep.release();
    //}
    //void GroundedCheck()
    //{
    //    Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance);
    //    if (hit.collider)
    //    {
    //        Grounded = true;
    //    }
    //    else
    //    {
    //        Grounded = false;
    //    }
    //}
}
