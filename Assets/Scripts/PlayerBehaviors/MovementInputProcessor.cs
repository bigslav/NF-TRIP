using UnityEngine;

public class MovementInputProcessor : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private MovementHandler _movementHandler = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed = 5f;

    private Vector3 _previousVelocity;
    private Vector2 _previousInputDirection;

    private Transform _mainCameraTransform;

    public Vector3 Value { get; private set; }

    private void OnEnable() => _movementHandler.AddModifier(this);
    private void OnDisable() => _movementHandler.RemoveModifier(this);

    private void Start() => _mainCameraTransform = Camera.main.transform;

    private void Update() => Move();

    public void SetMovementInput(Vector2 inputDirection)
    {
        _previousInputDirection = inputDirection;
    }

    private void Move()
    {
        float targetSpeed = _movementSpeed * _previousInputDirection.magnitude;

        Vector3 forward = _mainCameraTransform.forward;
        Vector3 right = _mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection;

        if (targetSpeed != 0f)
        {
            movementDirection = forward * _previousInputDirection.y + right * _previousInputDirection.x;
        }
        else 
        {
            movementDirection = _previousVelocity.normalized;
        }

        Value = movementDirection * targetSpeed;

        if (_animator != null)
        {
            if (Value.x != 0)
            {
                _animator.SetBool("IsWalking_b", true);
                _animator.SetBool("IsIdle_b", false);
            }
            else 
            {
                _animator.SetBool("IsWalking_b", false);
                _animator.SetBool("IsIdle_b", true);
            }

            if (_collisionProcessor.isGrounded == false)
            {
                _animator.SetBool("IsJumping_b", true);
            }
            else 
            {
                _animator.SetBool("IsJumping_b", false);
            }
        }

    }
}
