using UnityEngine;

public class Puller : MonoBehaviour
{
    private InputHandler _inputHandler;
    private CollisionProcessor _collisionProcessor;

    private bool _isTouchingMovable = false;
    private bool _isPulling = false;
    private bool _hasJoint = false;

    private GameObject _pulledObject = null;
    private Rigidbody _pulledObjectRb = null;
    private float _pulledObjectRbMass;

    private void OnEnable()
    {
        _inputHandler = GetComponent<InputHandler>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isTouchingMovable) {
            if (!_isPulling && _collisionProcessor.isGrounded)
            {
                _inputHandler.isPulling = true;
                _isPulling = true;
            }
            else if (_isPulling)
            {
                _inputHandler.isPulling = false;
                _isPulling = false;
                _pulledObjectRb.mass = _pulledObjectRbMass;
                _pulledObjectRb = null;
                Destroy(gameObject.GetComponent<FixedJoint>());
                _hasJoint = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Movable")
        {
            _isTouchingMovable = true;
            _pulledObject = collision.gameObject;
            _pulledObjectRb = _pulledObject.GetComponent<Rigidbody>();

            if (_isPulling && !_hasJoint)
            {
                gameObject.AddComponent<FixedJoint>();
                gameObject.GetComponent<FixedJoint>().connectedBody = _pulledObjectRb;
                _pulledObjectRbMass = _pulledObjectRb.mass;
                _pulledObjectRb.mass = 1;
                _hasJoint = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Movable")
        {
            _isTouchingMovable = false;
        }
    }
}