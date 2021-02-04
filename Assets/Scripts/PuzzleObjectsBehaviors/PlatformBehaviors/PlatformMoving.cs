using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : Platform
{
    [SerializeField] private Rigidbody _rb = null;

    public Vector3[] points;

    public float speed;
    public float delayTime;
    public bool automatic;

    private float _delayStart;
    private Vector3 _currentTarget;
    private int pointNumber;
    private float tolerance;

    void Start()
    {
        pointNumber = 0;
        if (points.Length > 0)
        {
            _currentTarget = points[0];
        }
        tolerance = speed * Time.deltaTime;
    }

    void FixedUpdate()
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

    private void MovePlatform() 
    {
        Vector3 heading = _currentTarget - transform.position;
/*        transform.position += (heading / heading.magnitude) * speed * Time.deltaTime;*/
        _rb.MovePosition(transform.position + (heading / heading.magnitude) * speed * Time.deltaTime);
        if (heading.magnitude < tolerance)
        {
            transform.position = _currentTarget;
            _delayStart = Time.time;
        }
    }

    private void UpdateTarget()
    {
        if (automatic)
        {
            if (Time.time - _delayStart > delayTime)
            {
                NextPlatform();
            }
        }
    }

    public void NextPlatform()
    {
        pointNumber++;
        if (pointNumber >= points.Length)
        {
            pointNumber = 0;
        }
        _currentTarget = points[pointNumber];
    }
}
