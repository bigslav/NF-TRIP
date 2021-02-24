using UnityEngine;
using System.Collections.Generic;

public class MovementHandler : MonoBehaviour
{
    private Rigidbody _rigidBody = null;
    private readonly List<IMovementModifier> _modifiers = new List<IMovementModifier>();

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() => Move();
    
    public void AddModifier(IMovementModifier modifier) => _modifiers.Add(modifier);

    public void RemoveModifier(IMovementModifier modifier) => _modifiers.Remove(modifier);

    private void Move() 
    {
        Vector3 movement = Vector3.zero;

        foreach (IMovementModifier modifier in _modifiers)
        {
            movement += modifier.Value;
        }

        if (_rigidBody != null)
        {
            _rigidBody.velocity = new Vector3(movement.x, _rigidBody.velocity.y);
        }
    }

    public void SetRigidbody(Rigidbody rb)
    {
        _rigidBody = rb;
    }

}
