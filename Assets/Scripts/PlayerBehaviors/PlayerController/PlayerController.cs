using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private float speed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float gravity;
	[SerializeField] private float rotationSpeed;

	[Header("States")]
	//private bool isGrounded;
	//public bool isControllerActive;

	[Header("Inner variables")]
	private Vector3 playerVelocity;
	private float xVelocity;
	private float targetRotation = 180;
	private float actualGravity;

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

		//JoinPlatform();
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
		actualGravity = gravity / 100;
		if (controller.isGrounded)
		{
			playerVelocity.y = -0.1f;
		} else
		{
			playerVelocity.y -= actualGravity * actualGravity;// Time.fixedDeltaTime;
		}
	}

	private void RefreshRotation(float xVelocity) // TODO: make it faster clos to the end and slower to start
	{
		if (xVelocity > 0)
		{
			targetRotation = Mathf.Lerp(targetRotation, 90, rotationSpeed * Time.deltaTime);
		} else if (xVelocity < 0)
		{
			targetRotation = Mathf.Lerp(targetRotation, 270, rotationSpeed * Time.deltaTime);
		} else
		{
			targetRotation = Mathf.Lerp(targetRotation, 180, rotationSpeed * Time.deltaTime);
		}

		transform.rotation = Quaternion.Euler(0, targetRotation, 0);
	}

	private void SetAnimation()
	{
		//animator.SetBool("isMoving", velocityX.x != 0);
	}

	private void JoinPlatform()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.1f))
		{
			if (hit.collider.gameObject.CompareTag("Respawn"))
			{
				transform.parent = hit.collider.gameObject.transform;
			}
		}
	}
}
