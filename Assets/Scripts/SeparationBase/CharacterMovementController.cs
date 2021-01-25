using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterMovementController : MonoBehaviour
{
    [HideInInspector]
    public bool golemIsActive;
    [HideInInspector]
    public bool mushroomIsActive;

    [SerializeField] private CinemachineVirtualCamera _golemCamera;
    [SerializeField] private CinemachineVirtualCamera _mushroomCamera;

    [SerializeField] private InputHandler _golemInputHandler;
    [SerializeField] private InputHandler _mushroomInputHandler;
    [SerializeField] private MovementInputProcessor _golemMovementInputProcessor;
    [SerializeField] private MovementInputProcessor _mushroomMovementInputProcessor;
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;
    [SerializeField] private CollisionProcessor _mushroomCollisionProcessor;
    [SerializeField] private Rigidbody _mushroomRigidbody;

    [SerializeField] private float riderSeatHeight;

    private bool _isJumpOffProcessed;

    private void Start()
    {
        SetActive("golem");
        SetInactive("mushroom");
        _isJumpOffProcessed = false;
        _golemCamera.Priority = 10;
        _mushroomCamera.Priority = 5;
    }

    private void Update()
    {
        HandleInput();

        ProcessJumpOnTop();

        ProcessRiding();

        HandleMushroomRotation();

        if (golemIsActive)
        {
            _golemCamera.Priority = 10;
            _mushroomCamera.Priority = 5;
        }
        else if (mushroomIsActive)
        {
            _golemCamera.Priority = 5;
            _mushroomCamera.Priority = 10;
        }
    }

    private void HandleMushroomRotation()
    {
        if (golemIsActive && _mushroomCollisionProcessor.isOnTopOfGolem)
        {
            if (_golemInputHandler.isFacingRight)
            {
                _mushroomGameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                _mushroomInputHandler.isFacingRight = true;
            }
            else
            {
                _mushroomGameObject.transform.eulerAngles = new Vector3(0, 180, 0);
                _mushroomInputHandler.isFacingRight = false;
            }
        }
    }

    public void SetActive(string characterName)
    {
        if (characterName == "golem")
        {
            _golemInputHandler.enabled = true;
            golemIsActive = true;
        }
        else if (characterName == "mushroom")
        {
            _mushroomInputHandler.enabled = true;
            mushroomIsActive = true;
        }
    }

    public void SetInactive(string characterName)
    {
        if (characterName == "golem")
        {
            _golemInputHandler.enabled = false;
            golemIsActive = false;
        }
        else if (characterName == "mushroom")
        {
            _mushroomInputHandler.enabled = false;
            mushroomIsActive = false;
        }
    }

    public void SetCharactersCollisions(bool isActive)
    {
        if (isActive)
        {
            Physics.IgnoreLayerCollision(8, 9, false);
        }
        else
        {
            Physics.IgnoreLayerCollision(8, 9);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchCharacters();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(JumpOnOff());
        }
    }

    private void SwitchCharacters()
    {
        _golemMovementInputProcessor.SetMovementInput(new Vector2(0, 0));
        _mushroomMovementInputProcessor.SetMovementInput(new Vector2(0, 0));

        if (_golemInputHandler.enabled == true)
        {
            SetInactive("golem");
            SetActive("mushroom");
        }
        else if (_mushroomInputHandler.enabled == true)
        {
            SetInactive("mushroom");
            SetActive("golem");
        }
    }

    private void ProcessJumpOnTop()
    {
        if (!_isJumpOffProcessed)
        {
            if ((_mushroomGameObject.transform.position.y - _golemGameObject.transform.position.y) > 1.85)
            {
                SetCharactersCollisions(true);
            }
            else
            {
                SetCharactersCollisions(false);
            }
        }
    }

    private IEnumerator JumpOnOff()
    {
        if (mushroomIsActive && _isJumpOffProcessed == false)
        {
            _isJumpOffProcessed = true;
            SetCharactersCollisions(false);
            yield return new WaitUntil(() => _mushroomCollisionProcessor.isGrounded == true);
            _isJumpOffProcessed = false;
        }
    }

    private void FixMushroomOnTop(bool value)
    {
        if (value)
        {
            _mushroomRigidbody.isKinematic = true;
            _mushroomGameObject.transform.position = new Vector3(_golemGameObject.transform.position.x, _golemGameObject.transform.position.y + riderSeatHeight, _golemGameObject.transform.position.z);
        }
        else
        {
            _mushroomRigidbody.isKinematic = false;
        }
    }

    private void ProcessRiding()
    {
        if (_mushroomCollisionProcessor.isOnTopOfGolem && !mushroomIsActive)
        {
            FixMushroomOnTop(true);
        }
        else
        {
            FixMushroomOnTop(false);
        }
    }
}
