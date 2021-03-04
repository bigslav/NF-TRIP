using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    public float sideRotationSpeed = 3f;
    public float intoIdleRotationSpeed = 5f;

    private Character _character;
    private bool twerkMode = false;
    private Animator _animator = null;
    private Rigidbody _rb = null;
    private float lastHorVelocity;

    private void OnEnable() {
        _character = GetComponent<Character>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            twerkMode = !twerkMode;
        }

        // Character rotations while moving
        if (!_character.isPulling)
        {
            if (_rb.velocity.x < 0)
            {
                _character.model.transform.rotation = Quaternion.Lerp(_character.model.transform.rotation, Quaternion.Euler(0, -90, 0), sideRotationSpeed * Time.deltaTime);
                _character.isFacingRight = false;
            }
            else if (_rb.velocity.x > 0)
            {
                _character.model.transform.rotation = Quaternion.Lerp(_character.model.transform.rotation, Quaternion.Euler(0, 90, 0), sideRotationSpeed * Time.deltaTime);
                _character.isFacingRight = true;
            }
            else if (_rb.velocity.x == 0f && lastHorVelocity < 0)
            {
                _character.model.transform.rotation = Quaternion.Lerp(_character.model.transform.rotation, Quaternion.Euler(0, -135, 0), intoIdleRotationSpeed * Time.deltaTime);
            }
            else if (_rb.velocity.x == 0f && lastHorVelocity > 0)
            {
                _character.model.transform.rotation = Quaternion.Lerp(_character.model.transform.rotation, Quaternion.Euler(0, 135, 0), intoIdleRotationSpeed * Time.deltaTime);
            }
        }

        // Movement animations
        if (_animator != null)
        {
            if (_rb.velocity.x != 0 && _character.isActive)
            {
                _animator.SetBool("IsWalking_b", true);
                _animator.SetBool("IsIdle_b", false);
            }
            else
            {
                _animator.SetBool("IsWalking_b", false);
                _animator.SetBool("IsIdle_b", true);
            }

            if (_character.isGrounded || _character.isOnTopOfGolem)
            {
                _animator.SetBool("IsJumping_b", false);
            }
            else
            {
                _animator.SetBool("IsJumping_b", true);
            }

            if (twerkMode)
            {
                _animator.SetBool("IsTwerking_b", true);
            }
            else
            {
                _animator.SetBool("IsTwerking_b", false);
            }
        }

        if (_rb.velocity.x != 0f) 
        {
            lastHorVelocity = _rb.velocity.x;
        }
    }
}
