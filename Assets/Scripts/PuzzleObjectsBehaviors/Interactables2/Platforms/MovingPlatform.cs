using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : ParentPlatform
{
    public float rotationSpeed = 10f;
    public float movementSpeed = 10f;
    public float delayTime;
    public float waitUntilTime; 

    public Transform[] points;
    public int blockedPoint;

    private float _delayStart;
    public Vector3 _currentTarget;
    public int pointNumber;

    private float tolerance;


    void Start()
    {
        active = activeAtStart; 
        pointNumber = 0;
        if (points.Length > 0)
        {
            _currentTarget = points[0].position;
        }

        tolerance = movementSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.transform);
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if (!parentingDisabled)
                if (collision.gameObject.transform.parent == null)
                {
                    collision.gameObject.transform.parent = transform;
                }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            if(!parentingDisabled)
                if (collision.gameObject.transform.parent == transform)
                {
                    collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collision.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
                    collision.gameObject.transform.parent = null;
                }
        }
    }

    public void MovePlatform()
    {
        Vector3 heading = _currentTarget - transform.position;
        transform.position += (heading / heading.magnitude) * movementSpeed * Time.deltaTime;
        if (heading.magnitude < tolerance)
        {
            waitUntilTime = Time.time + delayTime;
            transform.position = _currentTarget;
            _delayStart = Time.time;
        }
    }

    private void UpdateTarget()
    {
        if (waitForNextInput)
        {
            active = false;
        }
      
        NextPlatform();
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
                    if (oneWay)
                    {
                        if (oneWaySwitch) 
                        {
                            pointNumber = 0;
                            _currentTarget = points[pointNumber].position;
                        }
                        active = false;
                        return;
                    }
                    else
                    {
                        pointNumber = 0;
                    }
                }
                else 
                {
                    pointNumber = points.Length - 2;
                }
            }

            _currentTarget = points[pointNumber].position;
        }
    }
}
