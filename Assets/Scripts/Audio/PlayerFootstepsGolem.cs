using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepsGolem : MonoBehaviour
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
    private float RayDistance = 2.3f;
    public string[] MaterialTypes;
    private int DefulatMaterialValue = 5;

    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;

    Animator anim;
    void Start()
    {
        //GroundedPlayed = false;
        anim = GetComponent<Animator>();
        AddEvent(1, 0.01f, "Step", 0); // WalkG
        AddEvent(1, 0.85f, "Step", 0);
        //AddEvent(2, 0.05f, "Jump", 0);
    }

    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        anim = GetComponent<Animator>();
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = anim.runtimeAnimatorController.animationClips[Clip];
        //Debug.Log("" + anim.runtimeAnimatorController.animationClips.Length);
        clip.AddEvent(animationEvent);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue);       // Draws a blue line down from wherever the player is in the game, so that you can see how far the raycast (that we're about to cast in this script) will travel for from the 'Scene' tab in Unity.
    }

    public void Step()
    {

        //Debug.Log("STEP");
        MaterialCheck();
        PlayFootstep();
    }

    void MaterialCheck()
    {
        //Debug.Log("transform.position: " + transform.position + "RayDistance: " + RayDistance);

        F_MaterialValue = DefulatMaterialValue;


        //if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))                                 // A raycast is fired down, from the position that the player is curenntly standing at, traveling as far as we decide to set the 'RayDistance' variable to. Infomration about the object it comes into contact with will then be stored inside the 'hit' variable for us to access.
        //{
        //    if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())                                    // Using the 'hit' varibale, we check to see if the raycast has hit a collider attached to a gameobject, that also has the 'FMODStudioMaterialSetter' script attached to it as a component...
        //    {
        //        F_MaterialValue = hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>().MaterialValue;    // ...and if it did, we then set our 'F_MaterialValue' varibale to match whatever value the 'MaterialValue' variable (which is inside the 'F_MaterialValue' varibale) is currently set to.
        //    }
        //    else                                                                                                     // Else if however, the player is standing on an object that doesn't have a 'FMODStudioMaterialSetter' script component for our raycast to find...
        //        F_MaterialValue = DefulatMaterialValue;                                                              // ...we then set 'F_MaterialValue' to match the value of 'DefulatMaterialValue'. 'DefulatMaterialValue' is given a value by the 'FMODStudioFootstepsEditor' script. This value represents whatever material we have selected as our 'DefulatMaterial' in the Unity Inspector tab.
        //}
        //else                                                                                                         // Else if however, the raycast can't find a collider attached to the object at all...
        //{
        //    Debug.Log("F_MaterialValue = DefulatMaterialValue: " + DefulatMaterialValue);
        //}
    }

    void PlayFootstep()
    {
        //Debug.Log("F_MaterialValue: " + F_MaterialValue);
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/char/golem/step");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }
}
