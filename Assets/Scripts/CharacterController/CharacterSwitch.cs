using System.Collections;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;

    public bool switchControlOn;
    public bool combineOn;
    public bool separationImpulseOn;

    private Character _golemCharacter;
    private Character _mushroomCharacter;
    private Rigidbody _golemRigidbody;
    private Rigidbody _mushroomRigidbody;
    private SideMovement _golemSideMovement;
    private SideMovement _mushroomSideMovement;
    
    private bool _isJumpOffProcessed = false;
    private FixedJoint joint = null;
    private FixedJoint _joint = null;

    private void OnEnable()
    {
        Physics.IgnoreLayerCollision(8, 10, false);
        Physics.IgnoreLayerCollision(8, 9, false);

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

        if (_mushroomCharacter.isCombined == true)
        {
            _mushroomCharacter.isGrounded = true;
        }
    }

    public void SwitchCharacterControl()
    {
        if (switchControlOn) 
        {
            _golemSideMovement.SetDirection(Vector3.zero);
            _mushroomSideMovement.SetDirection(Vector3.zero);

            _golemCharacter.isActive = !_golemCharacter.isActive;
            _mushroomCharacter.isActive = !_mushroomCharacter.isActive;
        }
    }

    public IEnumerator JumpOffGolem()
    {
        if (_mushroomCharacter.isActive && _isJumpOffProcessed == false)
        {
            _isJumpOffProcessed = true;
            Physics.IgnoreLayerCollision(8, 9, true);
            yield return new WaitUntil(() => _mushroomCharacter.isGrounded == true);
            _isJumpOffProcessed = false;
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.S) && Time.timeScale == 1)
        {
            StartCoroutine(JumpOffGolem());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.timeScale == 1)
        {
            Vector3 dist = _golemGameObject.transform.position - _mushroomGameObject.transform.position;

            Debug.Log(Mathf.Abs(dist.x) +" "+ Mathf.Abs(dist.y));

            if (Mathf.Abs(dist.x) < 2f && Mathf.Abs(dist.y) < 5f)
            {

                if (!(_golemCharacter.isCombined && _mushroomCharacter.isCombined))
                {
                    if (_mushroomCharacter.isActive)
                    {
                        SwitchCharacterControl();
                    }
                    Combine(true);
                    SwitchSound();
                }
                else
                {
                    SwitchCharacterControl();
                    Combine(false);
                    SwitchSound();
                }
            }
            else
            {
                SwitchCharacterControl();
                SwitchSound();
            }
        }
    }

    void SwitchSound()
    {
        FMOD.Studio.EventInstance switchEvent = FMODUnity.RuntimeManager.CreateInstance("event:/ui/gameplay/switch");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(switchEvent, transform, GetComponent<Rigidbody>());
        switchEvent.start();
        switchEvent.release();
    }

    private void ProcessJumpOnTop()
    {
        if (!_isJumpOffProcessed)
        {
            if ((_mushroomGameObject.transform.position.y - _golemGameObject.transform.position.y) > 3.9f)
            {
                Physics.IgnoreLayerCollision(8, 9, false);
            }
            else
            {
                Physics.IgnoreLayerCollision(8, 9, true);
            }
        }
    }

    public void Combine(bool turnOn) 
    {
        if (combineOn) 
        {
            if (turnOn)
            {
                if (joint == null)
                {
                    Physics.IgnoreLayerCollision(8, 10, true);
                    joint = _golemGameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = _mushroomRigidbody;

                    joint.autoConfigureConnectedAnchor = false;
                    joint.anchor = new Vector3(0, 4.1f, 0);
                    joint.connectedAnchor = Vector3.zero;
                    joint.massScale = 0.001f;
                    joint.enableCollision = true;

                    _golemCharacter.isCombined = true;
                    _mushroomCharacter.isCombined = true;
                    _golemCharacter.isGlueToMechanism = false;
                    _mushroomCharacter.isGlueToMechanism = false;
                }
            }
            else
            {
                Physics.IgnoreLayerCollision(8, 10, false);
                Destroy(joint);
                joint = null;

                _golemCharacter.isCombined = false;
                _mushroomCharacter.isCombined = false;

                if (separationImpulseOn && !_golemCharacter.isGrounded)
                {
                    _mushroomRigidbody.AddForce(Vector3.up * 6f, ForceMode.Impulse);
                }
            }
        }
    }
}
