using UnityEngine;

public class Puller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputHandler pullerInputHandler;
    [SerializeField] private MovementInputProcessor pullerMovementInputProcessor;

    private GameObject _objectToPull = null;
    private BoxMovementProcessor _objectToPullBoxMovementProcessor = null;

    private void Update()
    {
        if (_objectToPull != null && Input.GetKey(KeyCode.E))
        {
            pullerInputHandler.isPulling = true;
            _objectToPullBoxMovementProcessor.SetValue(pullerMovementInputProcessor.Value);
        }
        else if (_objectToPull != null)
        {
            pullerInputHandler.isPulling = false;
            _objectToPullBoxMovementProcessor.SetValue(Vector3.zero);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BoxMovementProcessor box))
        {
            _objectToPull = other.gameObject;
            _objectToPullBoxMovementProcessor = box;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BoxMovementProcessor box))
        {
            _objectToPull = null;
            _objectToPullBoxMovementProcessor = null;
        }
    }
}