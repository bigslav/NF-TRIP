using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayerHandler : MonoBehaviour
{
    private PlayerController player;

	[SerializeField] private KeyCode keyJump;
	[SerializeField] private KeyCode keySplit;
	[SerializeField] private KeyCode keyUnite;

	private void Start()
	{
		player = GetComponent<PlayerController>();
	}
	
	void Update()
    {
		player.MoveHorizontal(Input.GetAxis("Horizontal"));

		if (Input.GetKeyDown(keyJump))
		{
			Debug.Log("YES");
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
