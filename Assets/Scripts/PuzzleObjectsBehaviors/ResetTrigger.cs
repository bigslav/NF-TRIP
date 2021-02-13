using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    public Vector3[] position;
    public GameObject[] gameObject;


    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < position.Length; ++i)
        {
            if (other.gameObject == gameObject[i])
            {
                gameObject[i].transform.position = position[i];
            }
        }
    }
}