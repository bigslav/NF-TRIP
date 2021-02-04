using UnityEngine;

public class Puller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputHandler pullerInputHandler;
    [SerializeField] private MovementInputProcessor pullerMovementInputProcessor;
    [SerializeField] private BoxMovementProcessor boxMovementInputProcessor = null;

    private float _pullSpeed;
    private Vector2 _pullDirection;
    private GameObject _objectToPull;
    private Rigidbody _objectToPullRigidbody;

    private void Awake()
    {
        _objectToPull = null;
        _objectToPullRigidbody = null;
    }

    private void Update()
    {
        if (pullerInputHandler.isFacingRight)
        {
            _pullDirection = new Vector2(-1, 0);
        }
        else if (!pullerInputHandler.isFacingRight)
        {
            _pullDirection = new Vector2(1, 0);
        }

        if (boxMovementInputProcessor != null && Input.GetKey(KeyCode.E))
        {
            pullerInputHandler.isPulling = true;
            boxMovementInputProcessor.SetValue(pullerMovementInputProcessor.Value);
        }
        else if (boxMovementInputProcessor != null)
        {
            pullerInputHandler.isPulling = false;
            boxMovementInputProcessor.SetValue(Vector3.zero);
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