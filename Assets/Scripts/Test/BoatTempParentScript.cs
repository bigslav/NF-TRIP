using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTempParentScript : MovingPlatform
{
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Enter" + other.gameObject.name);
		if (other.gameObject.CompareTag("Golem") || other.gameObject.CompareTag("Mushroom"))
		{
			if (other.gameObject.transform.parent == null)
			{
				other.gameObject.transform.parent = transform;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Exit" + other.gameObject.name);
		if (other.gameObject.CompareTag("Golem") || other.gameObject.CompareTag("Mushroom"))
		{
			if (other.gameObject.transform.parent == transform)
			{
				other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, other.gameObject.GetComponent<Rigidbody>().velocity.y, 0);
				other.gameObject.transform.parent = null;
			}
		}
	}
}
