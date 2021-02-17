using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeJointRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public float force = 5f;
    public float upTime = 10f;

    [HideInInspector]
    public bool _beingLifted = false;

    private void Update()
    {
        if (_beingLifted)
        {
            _rb.AddForce(transform.up * force, ForceMode.Acceleration);
        }
        else 
        {
            _rb.AddForce(-transform.up * force, ForceMode.Acceleration);
        }
    }

    public IEnumerator Activate()
    {
        _beingLifted = true;
        yield return new WaitForSeconds(upTime);
        _beingLifted = false;
    }
}
