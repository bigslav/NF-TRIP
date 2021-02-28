using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _golemLayer;

    public bool isGrounded;
    public bool isOnTopOfGolem = false;

    private Character _character;
    private BoxCollider _collider;
    private Bounds _colliderBounds;
    private float _skinWidth = 0.015f;

    private void OnEnable()
    {
        _character = GetComponent<Character>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        _colliderBounds = _collider.bounds;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 _raycastOrigin;

        if (_character.type == Character.CharacterType.Golem)
        {
            _raycastOrigin = transform.position + new Vector3(0, _skinWidth, 0);
        }
        else 
        {
            _raycastOrigin = transform.position - (_colliderBounds.size / 2 - new Vector3(0, _skinWidth, 0));
        }

        if (_character.isActive && (_character.type == Character.CharacterType.Golem ^
            _character.type == Character.CharacterType.Mushroom))
        {
            if (Physics.Raycast(_raycastOrigin, transform.TransformDirection(Vector3.down), out hit, 0.03f))
            {
                if (hit.collider.gameObject.layer == 10)
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }

                if (_character.type == Character.CharacterType.Mushroom && hit.collider.gameObject.layer == 9)
                {
                    isOnTopOfGolem = true;
                }
                else
                {
                    isOnTopOfGolem = false;
                }
            }
            else
            {
                isGrounded = false;
                isOnTopOfGolem = false;
            }
        } 
    }
}