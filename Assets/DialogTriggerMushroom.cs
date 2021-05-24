using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerMushroom : MonoBehaviour
{
	private Dialog dialog;
	private bool isMushroomInside = false;
	private bool isGolemInside = false;
	private bool isDialogPlayed = false;

	private void Start()
	{
		dialog = GetComponent<Dialog>();
	}

	private void PlayDialog()
	{
		DialogPlayer.instance.PlayDialog(dialog);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Mushroom"))
		{
			isMushroomInside = true;
		}

		if (isMushroomInside && !isDialogPlayed)
		{
			isDialogPlayed = true;
			PlayDialog();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Mushroom"))
		{
			isMushroomInside = false;
		}
	}
}
