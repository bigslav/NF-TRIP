using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Jump _jump;
    [SerializeField] private MovementInputProcessor _movementInputProcessor = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;
    [SerializeField] private ForceReciever _forceReciever = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private GameObject _playerModel = null;

    public bool isFacingRight;
    public bool isPulling;
    public bool isUsingMechanism;
    public bool glueToMechanism;
    public int preset = -1;

    public Interactable mechanismUnderControl;

    private float horizontalInput;

    private void Start()
    {
        isFacingRight = true;
        isPulling = false;
        isUsingMechanism = false;
        glueToMechanism = false;
    }

    void Update()
    {
        _playerModel.transform.rotation.Set(transform.rotation.w, 0, transform.rotation.y, 0);
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem))
        {
            _jump.OnJump();
        }

        if (!isPulling)
        {
            if(horizontalInput == -1)
            {
                _playerModel.transform.eulerAngles = new Vector3(0, -90, 0);
                isFacingRight = false;
            }
            else if(horizontalInput == 1)
            {
                _playerModel.transform.eulerAngles = new Vector3(0, 90, 0);
                isFacingRight = true;
            }
        }

        _movementInputProcessor.SetMovementInput(new Vector2(horizontalInput, 0f));

        if (Input.GetKeyDown(KeyCode.E) && isUsingMechanism)
            glueToMechanism = !glueToMechanism;
    }

    void FixedUpdate()
    {
        if (glueToMechanism)
        {
            if (horizontalInput == -1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[0];
            if (horizontalInput == 1)
                mechanismUnderControl._currentTarget = mechanismUnderControl.points[1];
            if ((horizontalInput != 0) && mechanismUnderControl._currentTarget != mechanismUnderControl.transform.position)
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
