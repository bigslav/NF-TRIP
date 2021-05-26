using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActivenessSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject[] platforms;
	public bool steelCageSound = false;

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
		if (steelCageSound)
        {
			PlayCageLanding();
		}
		else
        {
			PlayStoneButtonSound();
		}
	}

	void PlayCageLanding()
	{
		FMOD.Studio.EventInstance cageLanding = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/steel_cage");
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(cageLanding, transform, GetComponent<Rigidbody>());
		cageLanding.start();
		cageLanding.release();
	}

	void PlayStoneButtonSound()
	{
		FMOD.Studio.EventInstance button = FMODUnity.RuntimeManager.CreateInstance("event:/objects/cave/magic_button");
		FMODUnity.RuntimeManager.AttachInstanceToGameObject(button, transform, GetComponent<Rigidbody>());
		button.start();
		button.release();
	}
}
