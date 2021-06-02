using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaScript : MonoBehaviour
{
    FMOD.Studio.EventInstance magma;
    private bool magmaOneSound = false;
    // Start is called before the first frame update
    void Start()
    {
        magma = FMODUnity.RuntimeManager.CreateInstance("event:/env/amb/02_cave/02_magma");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Golem") && !magmaOneSound)
        {
            Debug.Log("Start magma: " + other.name);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(magma, transform, GetComponent<Rigidbody>());
            magma.start();
            magmaOneSound = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Golem"))
        {
            Debug.Log("Exit Magma Collider");
            magma.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            magma.release();
        }
    }

    public void StopMagma()
    {
        magma.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        magma.release();
        magmaOneSound = true;
    }
}
