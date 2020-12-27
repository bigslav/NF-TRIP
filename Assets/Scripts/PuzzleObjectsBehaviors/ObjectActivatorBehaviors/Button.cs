using UnityEngine;

public class Button : ObjectActivator
{
    [SerializeField]
    private GameObject targetObject;

    public bool deactivatorBehavior;

    private void OnTriggerEnter(Collider other)
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
}