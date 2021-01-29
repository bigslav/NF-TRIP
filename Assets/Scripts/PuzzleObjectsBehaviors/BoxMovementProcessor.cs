using UnityEngine;

public class BoxMovementProcessor : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField]
    private MovementHandler _movementHandler = null;

    private float _movementSpeed;
    private Vector2 _direction;

    public Vector3 Value { get; private set; }

    private void OnEnable() => _movementHandler.AddModifier(this);
    private void OnDisable() => _movementHandler.RemoveModifier(this);

    private void Update() => Move();

    public void SetValue(Vector3 value)
    {
        Value = value;
    }

    private void Move()
    {

    }
}