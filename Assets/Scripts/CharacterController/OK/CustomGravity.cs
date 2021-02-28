using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public static float globalGravity = -9.81f;
    public float gravityScale = 1.0f;
    public float fallMultiplier = 1.0f;

    private Rigidbody _rb;

    private void OnEnable() 
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void FixedUpdate() => ApplyGravity();

    private void ApplyGravity() 
    {
        if (_rb.velocity.y < 0)
        {
            gravityScale = fallMultiplier;
        }
        else
        {
            gravityScale = 1f;
        }

        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
