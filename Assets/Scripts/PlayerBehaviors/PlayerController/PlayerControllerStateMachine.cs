using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerStateMachine : MonoBehaviour
{
	[Header("Player Settings")]
	[SerializeField] private float walkSpeed;
	[SerializeField] private float jumpForce;
	[SerializeField] private float gravityModifier = 1f;
	//[SerializeField] private float walkCooldown;

	[Header("States")]
	[SerializeField] private MoveState moveState = MoveState.Idle;

	[Header("Inner variables")]
	//private Vector3 playerVelocity;
	private Vector3 velocityX;
	private Vector3 velocityY;
	//private float walkTime = 0;

	[Header("Cashed variables")]
	private CharacterController controller;
	private Animator animator;
	private bool isGrounded;

	// Start is called before the first frame update
	void Start()
    {
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		isGrounded = controller.isGrounded;

		if (moveState == MoveState.Jump)
		{
			if (!isGrounded)
			{
				controller.Move(velocityY);
				velocityY.y -= gravityModifier * Time.deltaTime;
			} else
			{
				Idle();
			}
		} else if (moveState == MoveState.Walk)
		{
			controller.Move(velocityX);
			if (velocityX.x <= 0.01)
			{
				Idle();
			}
		}

		Logging();
	}

	public void MoveXAxis(float xMovement)
	{
		if (moveState != MoveState.Jump)
		{
			moveState = MoveState.Walk;
			velocityX.x = xMovement * walkSpeed * Time.deltaTime;
			//walkTime = walkCooldown;
		}
	}

	public void Jump()
	{
		if (moveState != MoveState.Jump)
		{
			velocityY.y = jumpForce * 0.1f;
			moveState = MoveState.Jump;
		}
	}

	private void Idle()
	{
		moveState = MoveState.Idle;
		// animator
	}

	enum MoveState
	{
		Idle,
		Walk,
		Jump
	}

	void Logging()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Debug.Log("vel.y: " + velocityY);
			Debug.Log("isGrounded: " + isGrounded);
		}
	}

	void Log()
	{
		Debug.Log("vel.y: " + velocityY);
		Debug.Log("isGrounded: " + isGrounded);
	}
}
