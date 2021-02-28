using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public float speed = 5f;
    [HideInInspector]
    public bool jumpAllowed = false;

    private Rigidbody _rb;

    [Header("Playback Settings")]
    public string[] MaterialTypes;
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.3f;
    [HideInInspector] 
    public int DefulatMaterialValue;
    private string MaterialParameterName = "Terrain";

    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (jumpAllowed)
        {
            _rb.AddForce(Vector3.up * speed, ForceMode.Impulse);

            if (gameObject.name == "Golem")
            {
                MaterialCheck();
                PlayJumpGolem();
            }
            if (gameObject.name == "Mushroom")
            {
                MaterialCheck();
                PlayJumpMushroom();
            }

            jumpAllowed = false;
        }
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
    void PlayJumpGolem()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/char/golem/jump");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }
    void PlayJumpMushroom()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/char/mushroom/jump");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.start();
        Footstep.release();
    }
}
