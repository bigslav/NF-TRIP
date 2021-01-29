using UnityEngine;

public class PushPlate : ObjectActivator
{
    [Header("Settings")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool deactivatorBehavior;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerStay(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
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

    private void OnTriggerExit(Collider other)
    {
        if (whoCanInteract == (whoCanInteract | (1 << other.gameObject.layer)))
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
}
