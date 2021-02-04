using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSwitcher : MonoBehaviour
{
	private Character activeChar = Character.Golem;
	[SerializeField] private GameObject golem;
	[SerializeField] private GameObject mushroom;
	private PlayerController golemController;
	private PlayerController mushroomController;
    // Start is called before the first frame update
    void Start()
    {
		golemController = golem.GetComponent<PlayerController>();
		mushroomController = mushroom.GetComponent<PlayerController>();

		//golemController.isControllerActive = true;
		//mushroomController.isControllerActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			switch (activeChar)
			{
				case Character.Golem:
					activeChar = Character.Mushroom;
					//golemController.isControllerActive = false;
					//mushroomController.isControllerActive = true;
					return;
				case Character.Mushroom:
					activeChar = Character.Golem;
					//golemController.isControllerActive = true;
					//mushroomController.isControllerActive = false;
					return;
				default:
					return;
			}
		}
    }

	enum Character
	{
		Golem,
		Mushroom
	}
}
