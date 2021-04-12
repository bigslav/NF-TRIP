using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivenessSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject[] platforms;

	private void SwitchActiveStatus()
	{
		foreach (GameObject platform in platforms)
		{
			platform.SetActive(!platform.activeSelf)
;		}
	}

	private void OnTriggerEnter(Collider other)
	{
		SwitchActiveStatus();
	}
}
