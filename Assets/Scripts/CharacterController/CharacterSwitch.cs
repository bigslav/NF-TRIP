using System.Collections;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;

    private Character _golemCharacter;
    private Character _mushroomCharacter;
    private SideMovement _golemSideMovement;
    private SideMovement _mushroomSideMovement;
    private bool _isJumpOffProcessed = false;

    private void OnEnable()
    {
        _golemCharacter = _golemGameObject.GetComponent<Character>();
        _mushroomCharacter = _mushroomGameObject.GetComponent<Character>();
        _golemSideMovement = _golemGameObject.GetComponent<SideMovement>();
        _mushroomSideMovement = _mushroomGameObject.GetComponent<SideMovement>();
    }

    private void Update()
    {
        ProcessInput();
        ProcessJumpOnTop();
    }

    private void FixedUpdate()
    {
        ProcessRiding();
    }

    public void SwitchCharacterControl()
    {
        _golemSideMovement.SetDirection(Vector3.zero);
        _mushroomSideMovement.SetDirection(Vector3.zero);

        _golemCharacter.isActive = !_golemCharacter.isActive;
        _mushroomCharacter.isActive = !_mushroomCharacter.isActive;
    }

    public IEnumerator JumpOffGolem()
    {
        if (_mushroomCharacter.isActive && _isJumpOffProcessed == false)
        {
            _isJumpOffProcessed = true;
            IgnoreCharactersCollisions(true);
            yield return new WaitUntil(() => _mushroomCharacter.isGrounded == true);
            _isJumpOffProcessed = false;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchCharacterControl();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(JumpOffGolem());
        }
    }

    private void IgnoreCharactersCollisions(bool ignore)
    {
        Physics.IgnoreLayerCollision(8, 9, ignore);
    }

    private void ProcessJumpOnTop()
    {
        if (!_isJumpOffProcessed)
        {
            if ((_mushroomGameObject.transform.position.y - _golemGameObject.transform.position.y) > 4.3f)
            {
                IgnoreCharactersCollisions(false);
            }
            else
            {
                IgnoreCharactersCollisions(true);
            }
        }
    }

    private void ProcessRiding()
    {
        if (_mushroomCharacter.isOnTopOfGolem && !_mushroomCharacter.isActive)
        {
            IgnoreCharactersCollisions(true);
            _mushroomCharacter.rigidBody.isKinematic = true;
            Vector3 targetPosition = new Vector3(_golemGameObject.transform.position.x, _golemGameObject.transform.position.y + 4.4f, _golemGameObject.transform.position.z);
            _mushroomCharacter.rigidBody.MovePosition(targetPosition);
        }
        else
        {
            _mushroomCharacter.rigidBody.isKinematic = false;
        }
    }
}
