using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;
    [SerializeField] private GameObject _golemModel;
    [SerializeField] private GameObject _mushroomModel;

    private Character _mushroomCharacter;
    private InputHandler _mushroomInputHandler;
    private SideMovement _mushroomSideWalkMovementModifier;
    private CollisionProcessor _mushroomCollisionProcessor;
    private Rigidbody _mushroomRigidbody;
    private Jump _mushroomJumpMovementModifier;
    private CustomGravity _mushroomCustomGravity;

    private Character _golemCharacter;
    private InputHandler _golemInputHandler;
    private SideMovement _golemSideWalkMovementModifier;
    private Jump _golemJumpMovementModifier;

    private bool _isJumpOffProcessed;
    private Transform _supposedParent;

    private void OnEnable()
    {
        _mushroomCharacter = _mushroomGameObject.GetComponent<Character>();
        _mushroomInputHandler = _mushroomGameObject.GetComponent<InputHandler>();
        _mushroomSideWalkMovementModifier = _mushroomGameObject.GetComponent<SideMovement>();
        _mushroomCollisionProcessor = _mushroomGameObject.GetComponent<CollisionProcessor>();
        _mushroomRigidbody = _mushroomGameObject.GetComponent<Rigidbody>();
        _mushroomJumpMovementModifier = _mushroomGameObject.GetComponent<Jump>();

        _golemCharacter = _golemGameObject.GetComponent<Character>();
        _golemInputHandler = _golemGameObject.GetComponent<InputHandler>();
        _golemSideWalkMovementModifier = _golemGameObject.GetComponent<SideMovement>();
        _golemJumpMovementModifier = _golemGameObject.GetComponent<Jump>();

    }

    private void Start()
    {
        SetActive("golem");
        SetInactive("mushroom");
        _isJumpOffProcessed = false;
    }
    private void FixedUpdate()
    {
        ProcessRiding();
    }

    private void Update()
    {
        if (_mushroomGameObject.transform.parent != _golemGameObject.transform)
            _supposedParent = _mushroomGameObject.transform.parent;

        HandleInput();

        ProcessJumpOnTop();

        HandleMushroomRotation();
    }

    private void HandleMushroomRotation()
    {
        if (_golemCharacter.isActive && _mushroomCollisionProcessor.isOnTopOfGolem)
        {
            _mushroomModel.transform.eulerAngles = _golemModel.transform.eulerAngles;
        }
    }

    public void SetActive(string characterName)
    {
        if (characterName == "golem")
        {
            _golemInputHandler.enabled = true;
            _golemJumpMovementModifier.enabled = true;
            _golemCharacter.isActive = true;
        }
        else if (characterName == "mushroom")
        {
            _mushroomGameObject.transform.parent = _supposedParent;
            _mushroomInputHandler.enabled = true;
            _mushroomJumpMovementModifier.enabled = true;
            _mushroomCharacter.isActive = true;
        }
    }

    public void SetInactive(string characterName)
    {
        if (characterName == "golem")
        {
            _golemInputHandler.enabled = false;
            _golemJumpMovementModifier.enabled = false;
            _golemCharacter.isActive = false;
        }
        else if (characterName == "mushroom")
        {
            _mushroomInputHandler.enabled = false;
            _mushroomJumpMovementModifier.enabled = false;
            _mushroomCharacter.isActive = false;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
        _golemSideWalkMovementModifier.SetDirection(Vector3.zero);
        _mushroomSideWalkMovementModifier.SetDirection(Vector3.zero);

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
            if ((_mushroomGameObject.transform.position.y - _golemGameObject.transform.position.y) > 4.3f)
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
        if (_mushroomCharacter.isActive && _isJumpOffProcessed == false)
        {
            _isJumpOffProcessed = true;
            SetCharactersCollisions(false);
            yield return new WaitUntil(() => _mushroomCollisionProcessor.isGrounded == true);
            _isJumpOffProcessed = false;
        }
    }

    private void ProcessRiding()
    {
        if (_mushroomCollisionProcessor.isOnTopOfGolem && !_mushroomCharacter.isActive)
        {
            SetCharactersCollisions(false);
            _mushroomRigidbody.isKinematic = true;
            Vector3 currentPosition = _mushroomGameObject.transform.position;
            Vector3 targetPosition = new Vector3(_golemGameObject.transform.position.x, _golemGameObject.transform.position.y + 4.4f, _golemGameObject.transform.position.z);
            Vector3 newPosition = Vector3.Lerp(currentPosition, targetPosition, 0.2f);

            _mushroomRigidbody.MovePosition(targetPosition);
        }
        else
        {
            _mushroomRigidbody.isKinematic = false;
        }
    }
}
