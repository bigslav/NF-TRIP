using UnityEngine;

public class ForceReciever : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private MovementHandler _movementHandler = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;

    [Header("Settings")]
    [SerializeField] private float _mass = 1f;
    [SerializeField] private float _drag = 5f;

    private bool _wasGroundedLastFrame;

    public Vector3 Value { get; private set; }

    private void OnEnable() => _movementHandler.AddModifier(this);
    private void OnDisable() => _movementHandler.RemoveModifier(this);

    private void FixedUpdate()
    {
        if (!_wasGroundedLastFrame && _collisionProcessor.isGrounded)
        {
            Value = new Vector3(Value.x, 0f, Value.z);
        }

        _wasGroundedLastFrame = _collisionProcessor.isGrounded;

        if (Value.magnitude < 0.2f)
        {
            Value = Vector3.zero;
        }

        Value = Vector3.Lerp(Value, Vector3.zero, _drag * Time.deltaTime);
    }

    public void AddForce(Vector3 force) => Value += force / _mass;
}