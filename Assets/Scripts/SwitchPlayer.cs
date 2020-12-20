using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayer : MonoBehaviour
{
    [SerializeField] private PlayerMovement firstPlayerMovement;
    [SerializeField] private PlayerMovement secondPlayerMovement;

    private void Awake()
    {
        SetEnabledValue(firstPlayerMovement);
        SetEnabledValue(secondPlayerMovement);
    }

    private void Update()
    {
        SetEnabledValue(firstPlayerMovement);
        SetEnabledValue(secondPlayerMovement);

        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            Switch();
        }
    }

    private void Switch()
    {
        firstPlayerMovement.isActive = !firstPlayerMovement.isActive;
        secondPlayerMovement.isActive = !secondPlayerMovement.isActive;
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
