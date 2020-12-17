using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Controls _playerControls;
    [SerializeField] private Rigidbody playerRigidbody;
    private Vector3 moveDirection;
    private bool isGrounded;
    public float jumpSpeed;
    public float runSpeed;
    public bool isActive;

    private void Awake()
    {
        _playerControls = new Controls();
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Jump.performed += HandleJumpInput;
        _playerControls.Player.Move.performed += HandleMoveInput;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Jump.performed -= HandleJumpInput;
        _playerControls.Player.Move.performed -= HandleMoveInput;
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void HandleMoveInput(InputAction.CallbackContext context)
    {
        float moveDirectionOnX = _playerControls.Player.Move.ReadValue<float>();
        moveDirection = new Vector3(moveDirectionOnX, 0, 0);
    }

    private void Jump() 
    {
        if (isGrounded)
        {
            playerRigidbody.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Move()
    {
        playerRigidbody.MovePosition(transform.position + moveDirection * runSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") {
            isGrounded = true;
        }
    }
}
