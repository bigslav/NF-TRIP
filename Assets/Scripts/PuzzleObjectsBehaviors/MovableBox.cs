using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBox : MonoBehaviour
{
    private FixedJoint _joint = null;
    private bool touching = false;
    private Collision collision;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && touching)
            ProcessJoint();
    }

    private void OnCollisionStay(Collision cls)
    {
        collision = cls;
        if (collision.gameObject.layer == 8)
            touching = true;
    }

    private void OnCollisionExit(Collision cls)
    {
        collision = cls;
        if (collision.gameObject.layer == 8 && _joint != null)
        {
            Destroy(_joint);
            _joint = null;
        }
        touching = false;
    }

    private void ProcessJoint()
    {
        Debug.Log(_joint);
        if (_joint != null)
        {
            Destroy(_joint);
            _joint = null;
            touching = false;
        }
        else
        {
            _joint = gameObject.AddComponent<FixedJoint>();
            _joint.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
        }
    }
}
