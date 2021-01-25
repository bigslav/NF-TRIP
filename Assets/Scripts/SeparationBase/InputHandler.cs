using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementInputProcessor _movementInputProcessor = null;
    [SerializeField] private CollisionProcessor _collisionProcessor = null;
    [SerializeField] private ForceReciever _forceReciever = null;


    [Header("Settings")]
    [SerializeField] private float _jumpForce = 5;

    [HideInInspector]
    public bool isFacingRight;

    private void Start()
    {
        isFacingRight = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
/*
        if (horizontalInput != 0 && _animator != null)
        {
            _animator.SetBool("IsWalking", true);
        }
        else 
        {
            _animator.SetBool("IsWalking", false);
        }*/

        if (horizontalInput == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;
        }
        else if(horizontalInput == -1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;
        }

        _movementInputProcessor.SetMovementInput(new Vector2(horizontalInput, 0f));

        if (Input.GetKeyDown(KeyCode.W) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem))
        {
            _forceReciever.AddForce(_jumpForce * Vector3.up);
        }

    }
}
