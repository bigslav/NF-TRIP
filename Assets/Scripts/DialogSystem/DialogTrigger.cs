using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
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
		} else if (other.CompareTag("Golem"))
		{
			isGolemInside = true;
		}

		if (isGolemInside && isMushroomInside && !isDialogPlayed)
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
		} else if (other.CompareTag("Golem"))
		{
			isGolemInside = false;
		}
	}
}
