using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterType type;
    public bool isActive;
    public bool isGrounded = false;
    public bool isCombined = false;
    public bool isOnTopOfGolem = false;
    public bool isPulling = false;
    public bool isFacingRight = true;
    public bool isUsingMechanism = false;
    public bool isGlueToMechanism = false;
    public bool isOnTopOfMovable = false;

    public GameObject model = null;
    public Rigidbody rigidBody;

    public Sprite FaceActive = null;
    public Sprite FaceNotActive = null;
    public GameObject Icon = null;
    private Image IconRenderer = null;

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody>();
        IconRenderer = Icon.GetComponent<Image>();
    }

    public enum CharacterType
    {
        Golem,
        Mushroom
    }

    private void Update()
    {
        if (isActive)
        {
            IconRenderer.sprite = FaceActive;
        }
        else
        {
            IconRenderer.sprite = FaceNotActive;
        }
    }
}
