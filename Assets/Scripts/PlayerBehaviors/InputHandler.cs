using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _playerModel = null;
    public float sideRotationSpeed = 3f;
    public float intoIdleRotationSpeed = 5f;
    public bool verticalLiftControl = true;
    public int preset = -1;

    [HideInInspector]
    public bool isFacingRight;
    [HideInInspector]
    public bool isPulling;
    [HideInInspector]
    public bool isUsingMechanism;
    [HideInInspector]
    public bool glueToMechanism;
    [HideInInspector]
    public Interactable mechanismUnderControl;

    private Jump _jump = null;
    private MovementInputProcessor _movementInputProcessor = null;
    private CollisionProcessor _collisionProcessor = null;

    private float _horizontalInput;
    private float _verticalInput;
    private float _liftControlInput;
    private float _lastInputVertical;
    private float _lastInputHorizontal;

    private void OnEnable()
    {
        _jump = GetComponent<Jump>();
        _movementInputProcessor = GetComponent<MovementInputProcessor>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
    }

    private void Start()
    {
        isFacingRight = true;
        isPulling = false;
        isUsingMechanism = false;
        glueToMechanism = false;
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (!isPulling)
        {
            if (_horizontalInput == -1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, -90, 0), sideRotationSpeed * Time.deltaTime);
                isFacingRight = false;
            }
            else if (_horizontalInput == 1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, 90, 0), sideRotationSpeed * Time.deltaTime);
                isFacingRight = true;
            }
            else if(_horizontalInput == 0 && _lastInputHorizontal == -1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, -135, 0), intoIdleRotationSpeed * Time.deltaTime);
            }
            else if (_horizontalInput == 0 && _lastInputHorizontal == 1)
            {
                _playerModel.transform.rotation = Quaternion.Lerp(_playerModel.transform.rotation, Quaternion.Euler(0, 135, 0), intoIdleRotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && isUsingMechanism)
            glueToMechanism = !glueToMechanism;

        if (!glueToMechanism) 
        {
            _movementInputProcessor.SetMovementInput(new Vector2(_horizontalInput, 0f));
        }

        if (_horizontalInput != 0) 
        {
            _lastInputHorizontal = _horizontalInput;
        }
    }

    void FixedUpdate()
    {
        if (verticalLiftControl)
        {
            _liftControlInput = _verticalInput;
        }
        else
        {
            _liftControlInput = _horizontalInput;
        }

        if (glueToMechanism)
        {
            if (_liftControlInput == -1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[0];
            if (_liftControlInput == 1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[1];
            if ((_liftControlInput != 0) && mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position)
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
}
