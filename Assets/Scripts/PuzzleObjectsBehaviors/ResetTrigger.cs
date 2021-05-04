using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("A");
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<InputHandler>().Spawn();
        }
    }
}