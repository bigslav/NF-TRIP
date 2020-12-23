using UnityEngine;

public class PushPlate : ObjectActivator
{
    [SerializeField]
    private GameObject targetObject;

    public bool deactivatorBehavior;

    private void OnTriggerStay(Collider other)
    {
        if (!deactivatorBehavior)
        {
            Activate(targetObject);
        }
        else
        {
            Deactivate(targetObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!deactivatorBehavior)
        {
            Deactivate(targetObject);
        }
        else
        {
            Activate(targetObject);
        }
    }
}
