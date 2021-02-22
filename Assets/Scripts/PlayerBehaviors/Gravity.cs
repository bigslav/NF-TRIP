using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private MovementHandler _movementHandler = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;

    [Header("Settings")]
    [SerializeField] private float _groundedPullMagnitude = 50f;

    private readonly float _gravityMagnitude = Physics.gravity.y;

    private bool _wasGroundedLasFrame;

    public Vector3 Value { get; private set; }

    private void OnEnable() => _movementHandler.AddModifier(this);
    private void OnDisable() => _movementHandler.RemoveModifier(this);

    private void Update() => ProcessGravity();

    private void ProcessGravity() 
    {
        if (!_collisionProcessor.isGrounded && !_collisionProcessor.isOnTopOfGolem)
        {
            Value = new Vector3(Value.x, -_groundedPullMagnitude, Value.z);
        }
        else if (_wasGroundedLasFrame)
        {
            Value = Vector3.zero;
        }
        else
        {
            Value = new Vector3(Value.x, Value.y + _gravityMagnitude * Time.deltaTime, Value.z);
        }

        _wasGroundedLasFrame = _collisionProcessor.isGrounded;
    }
}
