using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Jump _jump;
    [SerializeField] private MovementInputProcessor _movementInputProcessor = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;
    [SerializeField] private GameObject _playerModel = null;

    public float sideRotationSpeed = 3f;
    public float intoIdleRotationSpeed = 5f;

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
}
