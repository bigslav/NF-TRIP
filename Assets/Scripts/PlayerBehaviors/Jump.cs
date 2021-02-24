using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public float jumpVelocity = 50f;
    public float jumpTime = 0.25f;

    private Rigidbody _rb = null;
    private CollisionProcessor _collisionProcessor = null;
    private InputHandler _inputHandler = null;
    private bool _isJumping = false;
    private bool _jumpButtonPressed = false;
    private Vector3 _jumpVector;

    [HideInInspector]
    public bool jumpAllowed = false;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _collisionProcessor = GetComponent<CollisionProcessor>();
        _inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        _jumpVector = Vector3.up * jumpVelocity;
    }

    private void Update()
    {
        if (jumpAllowed) 
        {
            _jumpButtonPressed = Input.GetKey(KeyCode.Space);

            if (Input.GetKeyDown(KeyCode.Space) && (_collisionProcessor.isGrounded || _collisionProcessor.isOnTopOfGolem) && !_inputHandler.glueToMechanism && !_isJumping)
            {
                _isJumping = true;
                StartCoroutine(JumpRoutine());
            }
        }
    }

    IEnumerator JumpRoutine()
    {
        // Physics-based jump should be refactored for FixedUpdate
        _rb.velocity = Vector3.zero;
        float timer = 0;

        while (_jumpButtonPressed && timer < jumpTime)
        {
            float proportionCompleted = timer / jumpTime;
            Vector3 thisFrameJumpVector = Vector3.Lerp(_jumpVector, Vector3.zero, proportionCompleted);
            _rb.AddForce(thisFrameJumpVector);
            timer += Time.deltaTime;
            yield return null;
        }

        _isJumping = false;
    }
}
