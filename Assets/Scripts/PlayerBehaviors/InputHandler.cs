using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementInputProcessor _movementInputProcessor = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;
    [SerializeField] private ForceReciever _forceReciever = null;


    [Header("Settings")]
    [SerializeField] private float _jumpForce = 5;

    [Header("FMOD Settings")]
    [SerializeField] [FMODUnity.EventRef] private string FootstepsEventPath;
    [SerializeField] [FMODUnity.EventRef] private string JumpingEventPath;
    private string MaterialParameterName = "Terrain";
    private string JumpOrLandParameterName = "Jump Or Land";

    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 1.3f;
    private string JumpInputName = "Jump";
    public string[] MaterialTypes;
    [HideInInspector] public int DefulatMaterialValue;

    //These variables are used to control when the player executes a footstep.
    private Vector3 PrevPos;
    private float DistanceTravelled;
    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;
    private int F_MaterialValue;
    //These booleans will hold values that tell us if the player is touching the ground currently and if they were touching it during the last frame.
    private bool PlayerTouchingGround;
    private bool PreviosulyTouchingGround;

    public bool isFacingRight;
    public bool isPulling;

    private void Start()
    {
        isFacingRight = true;
        isPulling = false;
    }

    void Update()
    {
        transform.rotation.Set(transform.rotation.w, 0, transform.rotation.y, 0);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        /*
                if (horizontalInput != 0 && _animator != null)
                {
                    _animator.SetBool("IsWalking", true);
                }
                else 
                {
                    _animator.SetBool("IsWalking", false);
                }*/

        if (!isPulling)
        {
            if (horizontalInput == 1)
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                isFacingRight = true;
            }
            else if (horizontalInput == -1)
            {
                transform.eulerAngles = new Vector3(0, -90, 0);
                isFacingRight = false;
            }
        }

        _movementInputProcessor.SetMovementInput(new Vector2(horizontalInput, 0f));

        if (Input.GetKeyDown(KeyCode.W) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem))
        {

            transform.parent = null;
            _forceReciever.AddForce(_jumpForce * Vector3.up);
        }

        GroundedCheck(); // ???

        if (horizontalInput != 0) // If the player's on the ground, then play a footstep
        {
            DistanceTravelled += (transform.position - PrevPos).magnitude; // A value of the length between past and present points.
            if (DistanceTravelled >= StepDistance)
            {
                MaterialCheck();
                PlayFootstep();
                DistanceTravelled = 0f;
            }
            PrevPos = transform.position;
        }

        if (Input.GetKeyDown(KeyCode.W) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem))
        {
            transform.parent = null;
            _forceReciever.AddForce(_jumpForce * Vector3.up);
            MaterialCheck();
            PlayJumpOrLand(true);
        }

        if (PreviosulyTouchingGround == false && PlayerTouchingGround) // If the player touches ground now, but didn't touch the frame back.
        {
            MaterialCheck();
            PlayJumpOrLand(false);
            DistanceTravelled = 0f;
        }
        PreviosulyTouchingGround = PlayerTouchingGround;
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

    void GroundedCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance);
        if (hit.collider)
        {
            PlayerTouchingGround = true;
        }
        else
        {
            PlayerTouchingGround = false;
        }
    }

    void PlayJumpOrLand(bool F_JumpLandCalc)
    {
        FMOD.Studio.EventInstance Jl = FMODUnity.RuntimeManager.CreateInstance(JumpingEventPath);         // First we create an FMOD event instance.
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Jl, transform, GetComponent<Rigidbody>());    // Next that event instance is told to play at the location that our player is currently at.
        Jl.setParameterByName(MaterialParameterName, F_MaterialValue);
        Jl.setParameterByName(JumpOrLandParameterName, F_JumpLandCalc ? 0f : 1f);
        Jl.start();
        Jl.release();                                                                                     // Releasing the instance from memory.
    }

    void PlayFootstep()
    {
        if (PlayerTouchingGround)
        {
            FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
            Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
            Footstep.start();
            Footstep.release();
            Debug.Log(F_MaterialValue + " : " + PrevPos);
        }
    }
}
