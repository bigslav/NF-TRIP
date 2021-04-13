using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatTempScript : MonoBehaviour
{
	private MovingPlatform boat;
	int characterNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
		boat = GetComponent<MovingPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Golem") || other.CompareTag("Mushroom"))
		{
			characterNumber++;
		}

		if (characterNumber == 2)
		{
			boat.Activate();
		}
	}
}
