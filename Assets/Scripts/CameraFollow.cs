using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private Transform target;
    private Vector3 offset;

    public float smoothSpeed = 0.15f;

    void Start()
    {
        target = targetObject.transform;
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
