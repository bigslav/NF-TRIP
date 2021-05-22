using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePlayer : MonoBehaviour
{
    FMOD.Studio.EventInstance snapshot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.name);
        //Debug.Log("" + GetComponentInChildren<BoxCollider>());
        //if (GameObject.Find("Mushroom").transform)
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision IN");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(snapshot, GetComponentInChildren<BoxCollider>().transform, GetComponentInChildren<Rigidbody>());
        snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Dialogue IN");
        switch (transform.name)
        {
            case "M1":
                FMOD.Studio.EventInstance m1 = FMODUnity.RuntimeManager.CreateInstance("event:/dialogue/mushroom village/M1");
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(m1, transform, GetComponent<Rigidbody>());
                m1.start();
                m1.release();
                break;

        }
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(snapshot, GetComponentInChildren<BoxCollider>().transform, GetComponentInChildren<Rigidbody>());
        snapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Dialogue IN");
        snapshot.start();
    }
}
