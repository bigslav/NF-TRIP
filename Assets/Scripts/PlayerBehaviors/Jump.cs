using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public float jumpVelocity = 5f;

    private Rigidbody _rb = null;
    private CollisionProcessor _collisionProcessor = null;
    private InputHandler _inputHandler = null;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem) && !_inputHandler.glueToMechanism)
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
    }

    public void SetRigidbody(Rigidbody rb)
    {
        _rb = rb;
    }
}
