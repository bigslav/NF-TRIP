using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isActive;
    public float runSpeed;
    public float jumpSpeed;
    public float fallMultiplier;

    [SerializeField]
    private BoxCollider playerCollider;
    [SerializeField]
    private Rigidbody playerRigidbody;

    private float moveDirection;
    private bool isPulling;
    private bool isRiding;
    private bool isGrounded;

    private void Awake()
    {
        isPulling = false;
/*        playerCollider = GetComponent<BoxCollider>();*/
    }

    private void Update()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        TryMove(moveDirection);

        if (CheckIfGrounded())
        {
            if (Input.GetKeyDown(KeyCode.W)) 
            {
                TryJump();
            }
        }
        else 
        {
            if(playerRigidbody.velocity.y < 0)
            {
                AddFallVelocity();
            }
        }
    }

    private void TryJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f))
        {
            playerRigidbody.velocity = Vector3.up * jumpSpeed;
        }
    }

    private void TryMove(float dir)
    {
        if (isPulling == false)
        {
            if (dir < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (dir > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        playerRigidbody.velocity = new Vector3(runSpeed * dir, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
    }

    private bool CheckIfGrounded() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y + 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            isRiding = false;
        }
        return isGrounded;
    }

    private void AddFallVelocity()
    {
        playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    public bool IsPulling
    {
        get 
        {
            return isPulling;
        }
        set
        {
            isPulling = value;
        }
    }

    public bool IsRiding
    {
        get
        {
            return isRiding;
        }
        set
        {
            isRiding = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            isGrounded = value;
        }
    }

    public float MoveDirection
    {
        get
        {
            return moveDirection;
        }
    }
}
