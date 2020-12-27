using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : ObjectActivator
{
    public GameObject[] targetObjects;
    private bool wasTouched;

    private void Awake()
    {
        wasTouched = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (wasTouched == false)
        {
            foreach (GameObject target in targetObjects)
            {
                if (target.active == true)
                {
                    Deactivate(target);
                }
                else if (target.active == false)
                {
                    Activate(target);
                }
            }
        }
        wasTouched = true;
    }

    private void OnTriggerExit(Collider other)
    {
        wasTouched = false;
    }
}
