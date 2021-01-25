using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement bigPlayerMovement;
    [SerializeField] private PlayerMovement littlePlayerMovement;

    private void Awake()
    {
        SetEnabledValue(bigPlayerMovement);
        SetEnabledValue(littlePlayerMovement);
    }

    private void Update()
    {
        SetEnabledValue(bigPlayerMovement);
        SetEnabledValue(littlePlayerMovement);

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            Switch();
        }
    }

    private void Switch()
    {
        bigPlayerMovement.isActive = !bigPlayerMovement.isActive;
        littlePlayerMovement.isActive = !littlePlayerMovement.isActive;
    }

    private void SetEnabledValue(PlayerMovement playerMovement) 
    {
        if (playerMovement.isActive)
        {
            playerMovement.enabled = true;
        }
        else
        {
            playerMovement.enabled = false;
        }
    }
}
