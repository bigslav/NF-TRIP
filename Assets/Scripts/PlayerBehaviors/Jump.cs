using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.3f;
    public string[] MaterialTypes;
    [HideInInspector] public int DefulatMaterialValue;
    private string MaterialParameterName = "Terrain";

    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;

    public float jumpVelocity = 5f;

    private Rigidbody _rb = null;
    private CollisionProcessor _collisionProcessor = null;
    private InputHandler _inputHandler = null;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem) && !_inputHandler.glueToMechanism && !_inputHandler.isPulling)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
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
        }
    }

    public void SetRigidbody(Rigidbody rb)
    {
        _rb = rb;
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
