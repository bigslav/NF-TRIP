using UnityEngine;

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
    public GameObject model = null;
    public Rigidbody rigidBody;

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public enum CharacterType
    {
        Golem,
        Mushroom
    }
}
