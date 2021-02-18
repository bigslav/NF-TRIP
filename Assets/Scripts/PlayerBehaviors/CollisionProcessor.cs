using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    public enum CharacterType
    {
        Golem,
        Mushroom
    }

    public CharacterType characterType;

    public bool isGrounded = false;
    public bool isOnTopOfGolem = false;

    [SerializeField] private CharacterMovementController characterMovementController;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _golemLayer;
    [SerializeField] private BoxCollider _collider;

    private Bounds _colliderBounds;
    private float _skinWidth = 0.015f;
    private RaycastHit _hit;
    private Vector3 _raycastOrigin;

    private void Start()
    {
        _colliderBounds = _collider.bounds;
    }

    private void Update()
    {

        if (characterType == CharacterType.Golem)
        {
            _raycastOrigin = transform.position;

            if (characterMovementController.golemIsActive)
            {
                if (Physics.CheckBox(_raycastOrigin, new Vector3(_colliderBounds.size.x / 2 - 0.015f, _colliderBounds.min.y + 0.015f, _colliderBounds.size.z / 2 - 0.015f), Quaternion.identity, _groundLayer))
                {
                    isGrounded = true;
                }
                else 
                {
                    isGrounded = false;
                }
            }
        }
        else if (characterType == CharacterType.Mushroom)
        {
            _raycastOrigin = transform.position + new Vector3(0, 1.2f, 0);

            if (characterMovementController.mushroomIsActive)
            {
                if (Physics.CheckBox(_raycastOrigin, new Vector3(_colliderBounds.size.x / 2 - 0.015f, _colliderBounds.min.y + 0.015f, _colliderBounds.size.z / 2 - 0.015f), Quaternion.identity, _groundLayer))
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }

                if (Physics.CheckBox(_raycastOrigin, new Vector3(_colliderBounds.size.x / 2 - 0.015f, _colliderBounds.min.y + 0.015f, _colliderBounds.size.z / 2 - 0.015f), Quaternion.identity, _golemLayer))
                {
                    isOnTopOfGolem = true;
                }
                else
                {
                    isOnTopOfGolem = false;
                }
            }
        }        
    }

    /* private void OnTriggerEnter(Collider collision)
     {
         if (collision.gameObject.layer == 10)
         {
             isGrounded = true;
         }

         if (collision.gameObject.layer == 11)
         {
             isOnTopOfGolem = true;
         }
     }

     private void OnTriggerStay(Collider collision)
     {
         if (collision.gameObject.layer == 10)
         {
             isGrounded = true;
         }

         if (collision.gameObject.layer == 11)
         {
             isOnTopOfGolem = true;
         }
     }

     private void OnTriggerExit(Collider collision)
     {
         if (collision.gameObject.layer == 10)
         {
             isGrounded = false;
         }

         if (collision.gameObject.layer == 11)
         {
             isOnTopOfGolem = false;
         }
     }*/


}