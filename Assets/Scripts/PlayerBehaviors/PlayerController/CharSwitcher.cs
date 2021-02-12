using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSwitcher : MonoBehaviour
{
	private Character activeChar = Character.Golem;
	[SerializeField] private GameObject golem;
	[SerializeField] private GameObject mushroom;
	private InputPlayerHandler golemController;
	private InputPlayerHandler mushroomController;
    // Start is called before the first frame update
    void Start()
    {
		golemController = golem.GetComponent<InputPlayerHandler>();
		mushroomController = mushroom.GetComponent<InputPlayerHandler>();

		golemController.isActive = true;
		mushroomController.isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			golemController.isActive = !golemController.isActive;
			mushroomController.isActive = !mushroomController.isActive;
			/*
			switch (activeChar)
			{
				case Character.Golem:
					activeChar = Character.Mushroom;
					golemController.enabled = false;
					mushroomController.enabled = true;
					return;
				case Character.Mushroom:
					activeChar = Character.Golem;
					golemController.enabled = true;
					mushroomController.enabled = false;
					return;
				default:
					return;
			}
			*/
		}
    }

	enum Character
	{
		Golem,
		Mushroom
	}
}
