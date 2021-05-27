using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool verticalLiftControl = true;
    public int preset = -1;

    [HideInInspector]
    public Interactable mechanismUnderControl;

    private Character _character;
    private Jump _jump;
    private SideMovement _sideMovement;

    private float _horizontalInput;
    private float _verticalInput;
    private float _liftControlInput;

    public GameObject pause;

    public GameObject[] listOfSpawnPoints;

    private bool landingSound = false;
    private string MaterialParameterName = "Terrain";
    public string[] MaterialTypes;
    [HideInInspector] public int DefulatMaterialValue = 1;
    private RaycastHit hit;
    private int F_MaterialValue;
    //[SerializeField] private float StepDistance = 2.0f;
    [SerializeField] private float RayDistance = 2.5f;
    private float timeForLanding = 0.0f;


    private void OnEnable()
    {
        _character = GetComponent<Character>();
        _jump = GetComponent<Jump>();
        _sideMovement = GetComponent<SideMovement>();
    }

    private void Start()
    {
        Debug.Log(LoaderWatchDog.wasLoaded);
        if (LoaderWatchDog.wasLoaded == 0)
        {
            Debug.Log("SPAWN");
            Spawn();
        }
        else
        {
            if (LoaderWatchDog.wasLoaded == 1)
            {
                CharacterSwitch characterSwitch = FindObjectsOfType<CharacterSwitch>()[0];
                characterSwitch.Combine(true);
            }
            --LoaderWatchDog.wasLoaded;
        }
    }

    public void Spawn()
    {
        string str = UnityEngine.StackTraceUtility.ExtractStackTrace();
        Debug.Log(str);
        gameObject.transform.position = listOfSpawnPoints[GlobalVariables.spawnToCheckointId].transform.position;
        _character.isFacingRight = true;
        _character.isPulling = false;
        _character.isUsingMechanism = false;
        _character.isGlueToMechanism = false;
    }

    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        ProcessMechanismControl();
    }

    private void ProcessInput()
    {
        if (_character.isActive)
        {
            timeForLanding -= Time.deltaTime;
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space) && (_character.isGrounded || _character.isOnTopOfGolem) && !_character.isGlueToMechanism && !_character.isPulling && Time.timeScale == 1)
            {
                _jump.jumpAllowed = true;
                landingSound = true;
                timeForLanding = 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.E) && _character.isUsingMechanism && Time.timeScale == 1)
            {
                _character.isGlueToMechanism = !_character.isGlueToMechanism;
                _sideMovement.SetDirection(new Vector3(0f, 0f));
            }

            if (!_character.isGlueToMechanism)
            {
                _sideMovement.SetDirection(new Vector3(_horizontalInput, 0f));
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                pause.SetActive(true);
            }

            if (landingSound && _character.isGrounded && timeForLanding <= 0.0f)
            {
                MaterialCheck();
                PlayLanding();
                landingSound = false;
            }
        }
    }

    private void ProcessMechanismControl() 
    {
        if (verticalLiftControl)
        {
            _liftControlInput = _verticalInput;
        }
        else
        {
            _liftControlInput = _horizontalInput;
        }

        if (_character.isGlueToMechanism)
        {
            if (_liftControlInput == -1 && _character.isActive)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[0].position;
            if (_liftControlInput == 1 && _character.isActive)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[1].position;
            if ((_liftControlInput != 0) && mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position && _character.isActive)
            {
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

    void PlayLanding()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/char/mushroom/jump");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
        Footstep.setParameterByName("Jump Or Land", 1);
        Footstep.start();
        Footstep.release();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 && gameObject.layer == 9)
            if (collision.gameObject.transform.parent == null)
            {
                collision.gameObject.transform.parent = transform;
            }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8 && gameObject.layer == 9)
            if (collision.gameObject.transform.parent == transform)
            {
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collision.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
                collision.gameObject.transform.parent = null;
            }
    }
}