using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatf : MonoBehaviour
{
	float distance;
	bool directionRight = true;
    // Start is called before the first frame update
    void Start()
    {
		distance = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (directionRight)
		{
			transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
			distance += Time.deltaTime;
			if (distance > 3)
			{
				directionRight = false;
			}
		} else
		{
			transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
			distance -= Time.deltaTime;
			if (distance < 0)
			{
				directionRight = true;
			}
		}
		
    }
}
