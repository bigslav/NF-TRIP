using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public void Activate(GameObject targetObject)
    {
        targetObject.SetActive(true);
    }

    public void Deactivate(GameObject targetObject)
    {
        targetObject.SetActive(false);
    }
}
