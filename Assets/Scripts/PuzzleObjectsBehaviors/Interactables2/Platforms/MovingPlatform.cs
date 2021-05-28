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

    private float _delayStart;
    public Vector3 _currentTarget;

    public int pointNumber;
    public bool previousOperationForward = true;


    public float tolerance;
    public bool m_HitDetect;
    public float m_MaxDistance = 0.5f;

    [SerializeField]
    private Collider m_Collider;
    private RaycastHit m_Hit;

    private bool soundPlayed = false;
    public bool noSoundOnCollision = false;
    public bool sinkStoneSound = false;
    public bool boatSound = false;
    public bool noSound = false;

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
        if (!noSound)
        {
            soundPlayed = false;
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
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!noSound)
        {
            //Debug.Log("Exit");
            if (!soundPlayed && !noSoundOnCollision)
            {
                if (sinkStoneSound)
                {
                    playSinkStoneSound();
                }
                else if (boatSound)
                {
                    playBoatSound();
                }
                else
                {
                    playPlatformSound();
                }
                soundPlayed = true;
            }
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            {
                if (!parentingDisabled)
                    if (collision.gameObject.transform.parent == transform)
                    {
                        collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, collision.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
                        collision.gameObject.transform.parent = null;
                    }
            }
        }
    }

    public void MovePlatform()
    {
        if (Time.time > waitUntilTime)
        {
            Vector3 heading = _currentTarget - transform.position;

            m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, heading, out m_Hit, transform.rotation, m_MaxDistance);
            if (m_HitDetect && m_Hit.transform.gameObject.layer == 10)
            {
                if (previousOperationForward)
                {
                    pointNumber = pointNumber == 0 ? points.Length - 1 : pointNumber - 1;
                    previousOperationForward = false;
                }
                else
                {
                    pointNumber = pointNumber == points.Length ? 0 : pointNumber + 1;
                    previousOperationForward = true;
                }

                _currentTarget = points[pointNumber].position;
            }

            transform.position += (heading / heading.magnitude) * movementSpeed * Time.deltaTime;
            if (heading.magnitude < tolerance)
            {
                waitUntilTime = Time.time + delayTime;
                transform.position = _currentTarget;
                _delayStart = Time.time;
            }
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

           

            if (pointNumber >= points.Length)
            {
                if (oneWay)
                {

                    active = false;
                    return;

                }
                else
                {
                    pointNumber = 0;
                    previousOperationForward = false;
                }
            }

                previousOperationForward = true;
                _currentTarget = points[pointNumber].position;
                waitUntilTime = Time.time + delayTime;
                _delayStart = Time.time;
            }
    }
    private void playPlatformSound()
    {
        FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/in_out_platforms");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
        button.start();
        button.release();
    }

    private void playSinkStoneSound()
    {
        FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/sink_stones");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
        button.start();
        button.release();
    }
    private void playBoatSound()
    {
        FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/boat");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
        button.start();
        button.release();
    }
}
