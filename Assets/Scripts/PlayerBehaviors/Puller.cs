using UnityEngine;

public class Puller : MonoBehaviour
{
    public PlayerMovement pullerMovement;

    private float _pullSpeed;
    private float _pullDirection;
    private GameObject _objectToPull;
    private Rigidbody _objectToPullRigidbody;

    private void Awake()
    {
        _objectToPull = null;
        _objectToPullRigidbody = null;
    }
    private void Update()
    {
        _pullDirection = pullerMovement.MoveDirection;
        _pullSpeed = pullerMovement.runSpeed;

        if (_objectToPull != null && Input.GetKey(KeyCode.F))
        {
            pullerMovement.IsPulling = true;
            _objectToPullRigidbody.velocity = new Vector3(_pullSpeed * _pullDirection, _objectToPullRigidbody.velocity.y, _objectToPullRigidbody.velocity.z);
        }
        else 
        {
            pullerMovement.IsPulling = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "HeavyBox")
        {
            _objectToPull = other.gameObject;
            _objectToPullRigidbody = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "HeavyBox")
        {
            _objectToPull = null;
            _objectToPullRigidbody = null;
        }
    }
}
