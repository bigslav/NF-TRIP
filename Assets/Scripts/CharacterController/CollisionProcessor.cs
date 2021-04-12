using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _golemLayer;

    private Character _character;
    private CapsuleCollider _collider;
    private float _skinWidth = 0.015f;

    private void OnEnable()
    {
        _character = GetComponent<Character>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 _raycastOrigin;

        _raycastOrigin = _collider.center;
        
        if (_character.isActive && _character.type == Character.CharacterType.Mushroom)
        {
            if (Physics.Raycast(_raycastOrigin, transform.TransformDirection(Vector3.down), out hit, _collider.height/2 + _skinWidth))
            {
                if (hit.collider.gameObject.layer == 9)
                {
                    _character.isOnTopOfGolem = true;
                }
                else
                {
                    _character.isOnTopOfGolem = false;
                }
            }
            else
            {
                _character.isOnTopOfGolem = false;
            }
        } 
    }

    private void OnCollisionStay(Collision col)
    {
        foreach (ContactPoint p in col.contacts)
        {
            Vector3 bottom = _collider.bounds.center - (Vector3.up * _collider.bounds.extents.y);
            Vector3 curve = bottom + (Vector3.up * _collider.radius);
            Debug.DrawLine(curve, p.point, Color.blue, 0.5f);
            Vector3 dir = curve - p.point;

            if (dir.magnitude <= _collider.radius + 0.05f && Mathf.Abs(dir.x) < 0.28f && (col.gameObject.layer == 10 || (col.gameObject.layer == 9 && _character.type == Character.CharacterType.Mushroom)))
            {
                _character.isGrounded = true;
            }

        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 10 || (col.gameObject.layer == 9 && _character.type == Character.CharacterType.Mushroom))
        {
            _character.isGrounded = false;
        }
    }
}