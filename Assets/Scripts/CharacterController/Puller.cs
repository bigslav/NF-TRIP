using UnityEngine;

public class Puller : MonoBehaviour
{
    private Character _character;
    private bool _isTouchingMovable = false;
    private bool _hasJoint = false;
    private GameObject _pulledObject = null;
    private Rigidbody _pulledObjectRb = null;
    private float _pulledObjectRbMass;
    private FixedJoint _joint = null;

    private void OnEnable()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isTouchingMovable && Time.timeScale == 1)
        {
            if (!_character.isPulling && _character.isGrounded)
            {
                _character.isPulling = true;
            }
            else if (_character.isPulling)
            {
                _character.isPulling = false;
                _character.isPulling = false;
                _pulledObjectRb.mass = _pulledObjectRbMass;
                _pulledObjectRb = null;
                Destroy(_joint);
                _joint = null;
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
                _joint = gameObject.AddComponent<FixedJoint>();
                _joint.connectedBody = _pulledObjectRb;
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