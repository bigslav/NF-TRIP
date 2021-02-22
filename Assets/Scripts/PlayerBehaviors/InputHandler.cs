using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Jump _jump;
    [SerializeField] private MovementInputProcessor _movementInputProcessor = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;
    [SerializeField] private GameObject _playerModel = null;

    public float sideRotationSpeed = 3f;
    public float intoIdleRotationSpeed = 5f;

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

    [HideInInspector]
    public bool isFacingRight;
    [HideInInspector]
    public bool isPulling;

    public bool isUsingMechanism;
    [HideInInspector]
    public bool glueToMechanism;

    public int preset = -1;

    public Interactable mechanismUnderControl;

    private float horizontalInput;
    private float verticalInput;
    private float liftControlInput;
    public bool verticalLiftControl = true;

    private void Start()
    {
        isFacingRight = true;
        isPulling = false;
        isUsingMechanism = false;
        glueToMechanism = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.W) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem) && !isUsingMechanism)
        {
            _jump.OnJump();
        }

        if (!isPulling)
        {
            if (horizontalInput == -1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, -90, 0), sideRotationSpeed * Time.deltaTime);
                isFacingRight = false;
            }
            else if (horizontalInput == 1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, 90, 0), sideRotationSpeed * Time.deltaTime);
                isFacingRight = true;
            }
            else
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, 180, 0), intoIdleRotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isUsingMechanism)
            glueToMechanism = !glueToMechanism;

        if (!glueToMechanism) 
        {
            _movementInputProcessor.SetMovementInput(new Vector2(horizontalInput, 0f));
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

    void FixedUpdate()
    {
        if (verticalLiftControl)
        {
            liftControlInput = verticalInput;
        }
        else
        {
            liftControlInput = horizontalInput;
        }

        if (glueToMechanism)
        {
            if (liftControlInput == -1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[0];
            if (liftControlInput == 1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[1];
            if ((liftControlInput != 0) && mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position)
            {
                mechanismUnderControl.MovePlatform();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.T) && isUsingMechanism)
            {
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[preset];
                if (mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position)
                    mechanismUnderControl.MovePlatform();
            }
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
