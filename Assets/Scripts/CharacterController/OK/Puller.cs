using UnityEngine;

public class Puller : MonoBehaviour
{
    private Character _character;
    private CollisionProcessor _collisionProcessor;

    private bool _isTouchingMovable = false;
    private bool _hasJoint = false;

    private GameObject _pulledObject = null;
    private Rigidbody _pulledObjectRb = null;
    private float _pulledObjectRbMass;

    private void OnEnable()
    {
        _character = GetComponent<Character>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isTouchingMovable) {
            if (!_character.isPulling && _collisionProcessor.isGrounded)
            {
                _character.isPulling = true;
            }
            else if (_character.isPulling)
            {
                _character.isPulling = false;
                _character.isPulling = false;
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

            if (_character.isPulling && !_hasJoint)
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