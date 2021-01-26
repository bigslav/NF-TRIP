using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;

    private bool _movedToEnd;

    public float travelDuration = 2f;
    void Start()
    {
        transform.position = _startPos;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float counter = 0f;
        while (counter < travelDuration)
        {
            transform.position = Vector3.Lerp(_startPos, _endPos, counter / travelDuration);
            counter += Time.deltaTime;
        }
        transform.position = _endPos;
    }
}
