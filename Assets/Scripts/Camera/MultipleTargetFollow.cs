using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetFollow : MonoBehaviour
{
    [SerializeField] private Camera cam;

    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    public bool lockY = false;
    
    private Vector3 velocity;

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        Move();
        Zoom();
    }

    private void Move() 
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;
        Vector3 lockedPosition;

        if (lockY)
        {
            lockedPosition = new Vector3(newPosition.x, offset.y, newPosition.z);
        }
        else 
        {
            lockedPosition = newPosition;
        }

        transform.position = Vector3.SmoothDamp(transform.position, lockedPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
