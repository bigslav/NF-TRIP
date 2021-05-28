using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(GameObject.Find("Music"));
        Destroy(GameObject.Find("Ambience"));
        Destroy(GameObject.Find("Ambience (1)"));
        Destroy(GameObject.Find("Ambience-Cave"));
        Destroy(GameObject.Find("Ambience-Cave (1)"));
    }
}
