using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerHandler : MonoBehaviour
{
    private PlayerController player;

	[SerializeField] private KeyCode keyJump;
	[SerializeField] private KeyCode keySplit;
	[SerializeField] private KeyCode keyUnite;

	public bool isActive;

	private void Start()
	{
		player = GetComponent<PlayerController>();
	}
	
	void Update()
    {
		if (isActive)
		{
			player.MoveHorizontal(Input.GetAxis("Horizontal"));
		} else
		{
			player.MoveHorizontal(0);
		}

		if (Input.GetKeyDown(keyJump) && isActive)
		{
			player.Jump();
		}

		if (Input.GetKeyDown(keySplit))
		{
			
		}

		if (Input.GetKeyDown(keyUnite))
		{
			
		}
	}
}
