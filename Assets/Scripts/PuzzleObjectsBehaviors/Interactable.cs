using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb = null;

    public Vector3 m_EulerAngleVelocity;
    public float rotationSpeed = 10f;
    public float movementSpeed = 10f;
    public float delayTime;
    public float waitUntilTime; 
    public bool automatic;

    public Vector3[] points;
    public int blockedPoint;

    private float _delayStart;
    private Vector3 _currentTarget;
    private Vector3 _currentRotationTarget;
    private int pointNumber;

    private float tolerance;
    private float rotationTolerance;

    private bool active = false;
    public bool activeAtStart = false;
    public bool justRotate = false;

    void Start()
    {
        active = activeAtStart; 
        pointNumber = 0;
        if (points.Length > 0)
        {
            _currentTarget = points[0];
        }

        tolerance = movementSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //Debug.Log(transform.eulerAngles.z);
        //Debug.Log(transform.position);
        //Debug.Log(_currentTarget);
        if (active)
        {
            if (justRotate)
                JustRotate();
            if (transform.position != _currentTarget)
            {
                MovePlatform();
            }
            else
            {
                UpdateTarget();
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform);
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
        Debug.Log("Exit");
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (collision.gameObject.transform.parent == transform.parent) ;
            {
                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collision.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
                collision.gameObject.transform.parent = null;
            }
        }
    }

    private void BringDown()
    {
        transform.Rotate(Vector3.forward * (-rotationSpeed * Time.deltaTime));

        if (transform.eulerAngles.z < 270)
        {
            rotationSpeed = 0;
        }
    }
    private void BringUp()
    {
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));

        if (transform.eulerAngles.z > 350)
        {
            rotationSpeed = 0;
        }
    }

    private void MovePlatform()
    {
        Vector3 heading = _currentTarget - transform.position;
        //transform.position += (heading / heading.magnitude) * movementSpeed * Time.deltaTime;
        _rb.MovePosition(transform.position + (heading / heading.magnitude) * movementSpeed * Time.deltaTime);
        if (heading.magnitude < tolerance)
        {
            waitUntilTime = Time.time + delayTime;
            transform.position = _currentTarget;
            _delayStart = Time.time;
        }
        /*if (justRotate == false)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }*/
    }
   
    private void JustRotate()
    {
        //Quaternion target = Quaternion.Euler(0, 0, rotationSpeed);
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotationSpeed);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
        //transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void UpdateTarget()
    {
        if (automatic)
        {
            NextPlatform();
        }
    }

    public void NextPlatform()
    {
        if (points.Length != 1)
            if (Time.time > waitUntilTime)
        {
            pointNumber++;
            
            if (pointNumber == blockedPoint)
            {
                pointNumber -= 2;
            }

            if (pointNumber >= points.Length)
            {
                if (blockedPoint == -1)
                {
                    pointNumber = 0;
                }
                else 
                {
                    pointNumber = points.Length - 2;
                }
            }

            _currentTarget = points[pointNumber];
        }
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }

    public void Switch()
    {
        active = !active;
    }

}
