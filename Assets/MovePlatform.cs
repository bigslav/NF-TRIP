using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnCollisionEnter(Collision coll)
	{
		coll.transform.parent = transform;
		Debug.Log("yes");
	}

	void OnCollisionExit(Collision coll)
	{
		coll.transform.parent = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		other.transform.parent = transform;
		Debug.Log("yes");		
	}

	void OnTriggerExit(Collider coll)
	{
		coll.transform.parent = null;
	}
}
