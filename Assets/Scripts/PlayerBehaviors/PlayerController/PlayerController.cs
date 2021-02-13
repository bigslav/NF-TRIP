using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Movement Settings")]
	[SerializeField] private float speed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float gravity;

	[Header("States")]
	//private bool isGrounded;
	//public bool isControllerActive;

	[Header("Inner variables")]
	private Vector3 playerVelocity;
	private float xVelocity;
	//private float yVelocity;

	[Header("Cashed variables")]
	private CharacterController controller;
	//private Animator animator;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		//animator = GetComponent<Animator>();
	}

	private void Update()
	{
		controller.Move(playerVelocity);

		ApplyGravity();

		RefreshRotation(xVelocity);
	}

	public void MoveHorizontal(float inputValue)
	{
		xVelocity = inputValue;
		playerVelocity.x = xVelocity * speed * Time.deltaTime;
	}

	public void Jump()
	{
		if (controller.isGrounded)
		{
			playerVelocity.y = jumpForce;
		}
	}

	private void ApplyGravity()
	{
		if (controller.isGrounded)
		{
			playerVelocity.y = -0.1f;
		} else
		{
			playerVelocity.y -= gravity * Time.deltaTime;
		}
	}

	private void RefreshRotation(float xVelocity)
	{
		float rotation = 180 - 90 * xVelocity;
		transform.rotation = Quaternion.Euler(0, rotation, 0);
	}

	private void SetAnimation()
	{
		//animator.SetBool("isMoving", velocityX.x != 0);
	}

	void Logging()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			//Debug.Log("vel.y: " + yVelocity);
			Debug.Log("isGrounded: " + controller.isGrounded);
		}
	}

	void Log()
	{
		//Debug.Log("vel.y: " + yVelocity);
		//Debug.Log("isGrounded: " + isGrounded);
	}
}
