using UnityEngine;

public class SideMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector3 _targetDirection;
    private Rigidbody _rb;

    private void OnEnable() => _rb = GetComponent<Rigidbody>();

    private void FixedUpdate() => ApplyMovement();

    public void SetDirection(Vector3 direction)
    {
        _targetDirection = speed * direction;
    }

    private void ApplyMovement()
    {
        _rb.AddForce(_targetDirection, ForceMode.VelocityChange);
        CancelHorizontalVelocity();
    }

    private void CancelHorizontalVelocity()
    {
        _rb.AddForce(Vector3.right * -_rb.velocity.x, ForceMode.VelocityChange);
    }
}
