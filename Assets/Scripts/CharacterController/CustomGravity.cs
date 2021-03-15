using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public static float globalGravity = -9.81f;
    
    public float jumpMultiplier = 1.0f;
    public float fallMultiplier = 1.0f;

    private Rigidbody _rb;
    private float _gravityMultiplier;

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
            _gravityMultiplier = fallMultiplier;
        }
        else
        {
            _gravityMultiplier = jumpMultiplier;
        }

        Vector3 gravity = globalGravity * _gravityMultiplier * Vector3.up;
        _rb.AddForce(gravity, ForceMode.Acceleration);
    }
}
