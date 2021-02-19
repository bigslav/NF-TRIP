using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPathBlocker : MonoBehaviour
{
    [SerializeField] private Interactable _lift;

    private int _blockedPoint;

    private void Start()
    {
        _blockedPoint = _lift.blockedPoint;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BoxMovementProcessor box))
        {
            _lift.blockedPoint = _blockedPoint;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BoxMovementProcessor box))
        {
            _lift.blockedPoint = -1;
        }
    }
}
