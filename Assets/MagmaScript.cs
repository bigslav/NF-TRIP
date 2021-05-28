using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaScript : MonoBehaviour
{
    FMOD.Studio.EventInstance magma;
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
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(magma, transform, GetComponent<Rigidbody>());
        magma.start();
        magma.release();
    }
    public void StopMagma()
    {
        Debug.Log("stopdialogue");
        magma.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
