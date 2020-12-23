using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isFalling;
    public float persistTime;
    public float disappearTime;

    [SerializeField]
    private MeshRenderer platformRenderer;
    [SerializeField]
    private BoxCollider platformCollider;

    private IEnumerator Disappear() 
    {
        yield return new WaitForSeconds(persistTime);
        Deactivate();
        yield return new WaitForSeconds(disappearTime);
        Activate();
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        if (isFalling == true) 
        {
            StartCoroutine(Disappear());
        }
    }

    public void Activate()
    {
        platformRenderer.enabled = true;
        platformCollider.enabled = true;
    }

    public void Deactivate()
    {
        platformRenderer.enabled = false;
        platformCollider.enabled = false;
    }
}