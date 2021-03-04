using System.Collections;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;

    private Character _golemCharacter;
    private Character _mushroomCharacter;
    private Rigidbody _golemRigidbody;
    private Rigidbody _mushroomRigidbody;
    private SideMovement _golemSideMovement;
    private SideMovement _mushroomSideMovement;
    
    private bool _isJumpOffProcessed = false;
    private bool _areCombined = false;
    private CharacterJoint joint = null;

    private void OnEnable()
    {
        _golemCharacter = _golemGameObject.GetComponent<Character>();
        _mushroomCharacter = _mushroomGameObject.GetComponent<Character>();
        _golemSideMovement = _golemGameObject.GetComponent<SideMovement>();
        _mushroomSideMovement = _mushroomGameObject.GetComponent<SideMovement>();
        _golemRigidbody = _golemGameObject.GetComponent<Rigidbody>();
        _mushroomRigidbody = _mushroomGameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ProcessInput();
        ProcessJumpOnTop();
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(JumpOffGolem());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Vector3 dist = _golemGameObject.transform.position - _mushroomGameObject.transform.position;

            Debug.Log(Mathf.Abs(dist.x) +" "+ Mathf.Abs(dist.y));

            if (Mathf.Abs(dist.x) < 2f && Mathf.Abs(dist.y) < 5f)
            {
                if (!_areCombined)
                {
                    if (_mushroomCharacter.isActive)
                    {
                        SwitchCharacterControl();
                    }
                    Combine(true);
                }
                else
                {
                    SwitchCharacterControl();
                    Combine(false);
                }
            }
            else
            {
                SwitchCharacterControl();
            }
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
            if ((_mushroomGameObject.transform.position.y - _golemGameObject.transform.position.y) > 3.9f)
            {
                IgnoreCharactersCollisions(false);
            }
            else
            {
                IgnoreCharactersCollisions(true);
            }
        }
    }

/*    private void ProcessRiding()
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
    }*/

    public void Combine(bool turnOn) 
    {
        if (turnOn)
        {
            if (joint == null)
            {
                joint = _golemGameObject.AddComponent<CharacterJoint>();
                joint.connectedBody = _mushroomRigidbody;

                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = new Vector3(0, 4f, 0);
                joint.connectedAnchor = Vector3.zero;
                joint.massScale = 0.001f;
                joint.enableCollision = true;

                _areCombined = true;
            }
        }
        else 
        {
            Destroy(joint);
            joint = null;

            _areCombined = false;
        }
    }
}
