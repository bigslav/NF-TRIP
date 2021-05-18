using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : ParentPlatform
{
    public int[] angles;
    private int pointNumber;
    public float rotationSpeed = 10f;
    public float delayTime;
    public float waitUntilTime;
    private int sign;

    private float _delayStart;
    public int _currentTarget;

    private float tolerance;
    private bool bridgeSoundPlayed = false;

    void Start()
    {
        active = activeAtStart;
        pointNumber = 0;
        if (angles.Length > 0)
        {
            _currentTarget = angles[0];
            float tmpAngle = transform.eulerAngles.z > 180 ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
            sign = (tmpAngle - _currentTarget) < 0 ? 1 : -1;
        }

        tolerance = rotationSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log(transform.eulerAngles.z);
        //Debug.Log(transform.position);
        //Debug.Log(_currentTarget);
        if (active)
        {
            if (Mathf.Abs(transform.eulerAngles.z - _currentTarget) < 0.001 || Mathf.Abs(transform.eulerAngles.z - 360 - _currentTarget) < 0.001)
            {
                UpdateTarget();
            }
            else
            {
                if (!bridgeSoundPlayed)
                {
                    bridgeSoundPlayed = true;
                    PlayBridgeSound();
                }
                RotateToAngle();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if(collision.gameObject.transform.parent == null);
            {
                collision.gameObject.transform.parent = transform.parent;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (collision.gameObject.transform.parent == transform.parent) ;
            {
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collision.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
                collision.gameObject.transform.parent = null;
            }
        }
    }
    
    private void UpdateTarget()
    {
        NextPlatform();
    }
    public void NextPlatform()
    {
        if (angles.Length != 1)
            if (Time.time > waitUntilTime)
            {
                pointNumber++;

                if (pointNumber >= angles.Length)
                {
                    if (oneWay)
                    {
                        active = false;
                        return;
                    }
                    else
                    {
                        pointNumber = 0;
                    }
                }
                _currentTarget = angles[pointNumber];
                float tmpAngle = transform.parent.eulerAngles.z > 180 ? transform.parent.eulerAngles.z - 360 : transform.parent.eulerAngles.z;
                int tmpCurTarget = _currentTarget < 0 ? 360 + _currentTarget : _currentTarget;
                Debug.Log(transform.parent.eulerAngles.z);
                Debug.Log(tmpAngle - tmpCurTarget);
                sign = ((tmpAngle - tmpCurTarget) < 0 && (tmpAngle - tmpCurTarget) > - 180) || ((tmpAngle - tmpCurTarget) > 90 && (tmpAngle - tmpCurTarget) < 270) ? 1 : -1;
                Debug.Log(sign);
            }
    }
    private void RotateToAngle()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime * sign);

        float tmpAngle = transform.eulerAngles.z > 180 ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        if (Mathf.Abs(tmpAngle - _currentTarget) < tolerance)
        {
            waitUntilTime = Time.time + delayTime;
            transform.rotation.Set(0, 0, 0, _currentTarget);// Rotate(0, 0, sign*Mathf.Abs(transform.parent.eulerAngles.z - _currentTarget));
            _delayStart = Time.time;
        }
    }
    void PlayBridgeSound()
    {
        FMOD.Studio.EventInstance bridgeSound = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/bridge");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(bridgeSound, transform, GetComponent<Rigidbody>());
        bridgeSound.start();
        bridgeSound.release();
    }
}
