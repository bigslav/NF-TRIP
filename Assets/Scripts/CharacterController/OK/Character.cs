using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterType type;
    public bool isActive;
    public bool isPulling = false;
    public bool isFacingRight = true;
    public bool isUsingMechanism = false;
    public bool isGlueToMechanism = false;
    public GameObject model = null;

    public enum CharacterType
    {
        Golem,
        Mushroom
    }
}
