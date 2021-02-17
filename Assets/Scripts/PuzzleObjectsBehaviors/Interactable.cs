using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb = null;

    public float rotationSpeed = 10f;
    public float movementSpeed = 10f;
    public float delayTime;
    public float waitUntilTime; 
    public bool automatic;

    public Vector3[] points;

    private float _delayStart;
    private Vector3 _currentTarget;
    private int pointNumber;
    private float tolerance;

    private bool active = false;

    void Start()
    {
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if(collision.gameObject.transform.parent.parent == null);
            {
                collision.gameObject.transform.parent.parent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (collision.gameObject.transform.parent.parent == transform) ;
            {
                collision.gameObject.transform.parent.parent = null;
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
    }

    private void RotateToAngle(float angle)
    {

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

        if (Time.time > waitUntilTime)
        {
            pointNumber++;
            if (pointNumber >= points.Length)
            {
                pointNumber = 0;
            }
            _currentTarget = points[pointNumber];
        }

    }

    public void Acivate()
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
