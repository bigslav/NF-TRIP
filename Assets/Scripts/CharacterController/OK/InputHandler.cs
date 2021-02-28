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
    private CollisionProcessor _collisionProcessor;

    private float _horizontalInput;
    private float _verticalInput;
    private float _liftControlInput;

    private void OnEnable()
    {
        _character = GetComponent<Character>();
        _jump = GetComponent<Jump>();
        _sideMovement = GetComponent<SideMovement>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
    }

    private void Start()
    {
        _character.isFacingRight = true;
        _character.isPulling = false;
        _character.isUsingMechanism = false;
        _character.isGlueToMechanism = false;
    }

    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem) && !_character.isGlueToMechanism && !_character.isPulling)
        {
            _jump.jumpAllowed = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && _character.isUsingMechanism)
            _character.isGlueToMechanism = !_character.isGlueToMechanism;

        if (!_character.isGlueToMechanism) 
        {
            _sideMovement.SetDirection(new Vector3(_horizontalInput, 0f));
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

        if (_character.isGlueToMechanism)
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
            if (Input.GetKey(KeyCode.U) && _character.isUsingMechanism)
            {
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[preset];
                if (mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position)
                    mechanismUnderControl.MovePlatform();
            }
        }
    }
}
