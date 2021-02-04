using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : ObjectActivator
{
    [Header("Settings")]
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private LayerMask whoCanInteract;

    private bool wasTouched;

    private void Awake()
    {
        wasTouched = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
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
        }

        wasTouched = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
        {
            wasTouched = false;
        }
    }
}
