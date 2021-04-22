using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int id = -1;
    public SaveSystem saveSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        saveSystem = gameObject.transform.parent.Find("SaveManager").GetComponent<SaveSystem>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
        {
            saveSystem.localCheckpontNum = id;
            saveSystem.WriteCheckpoint();
        }
    }
}