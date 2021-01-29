using UnityEngine;

public class Button : ObjectActivator
{
    [Header("Settings")]
    [SerializeField] private GameObject targetObject;
    [SerializeField] private bool deactivatorBehavior;
    [SerializeField] private LayerMask whoCanInteract;

    private void OnTriggerEnter(Collider other)
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
}
