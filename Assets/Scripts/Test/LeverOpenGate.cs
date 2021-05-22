using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverOpenGate : MonoBehaviour
{
    [SerializeField] private LayerMask whoCanInteract;

    [SerializeField] private GameObject gateDoor;
    [SerializeField] private GameObject lever;

    [SerializeField] private GameObject pressETip;
    [SerializeField] private Sprite pressERed;
    [SerializeField] private Sprite pressEGreen;

    private SpriteRenderer _tipRenderer;
    private bool _openGateTriggered = false;

    private bool gateSoundTriggered = false;

    private void Start()
    {
        _tipRenderer = pressETip.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if(_openGateTriggered && (gateDoor.transform.rotation.eulerAngles.y <= 270f && gateDoor.transform.rotation.eulerAngles.y >= 140f))
        {
            gateDoor.transform.Rotate(0f, -2f, 0f, Space.Self);
        }
        //Debug.Log(lever.transform.rotation.eulerAngles.z);
        if (_openGateTriggered && (lever.transform.rotation.eulerAngles.z <= 360f && lever.transform.rotation.eulerAngles.z >= 285f))
        {
            lever.transform.Rotate(0f, 0f, -1f, Space.Self);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _tipRenderer.sprite = pressEGreen;
    }

    private void OnTriggerExit(Collider other)
    {
        _tipRenderer.sprite = pressERed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
            {
                if (!_openGateTriggered) 
                {
                    _openGateTriggered = true;
                    if (!gateSoundTriggered)
                    {
                        PlayGateSound();
                        gateSoundTriggered = true;
                    }
                }
            }
        }
    }
    void PlayGateSound()
    {
        FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance("event:/objects/mushroon village/wooden_gate");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());
        Footstep.start();
        Footstep.release();
    }
}
