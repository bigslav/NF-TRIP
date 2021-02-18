using UnityEngine;

public class CollisionProcessor : MonoBehaviour
{
    public enum CharacterType
    {
        Golem,
        Mushroom
    }

    public CharacterType characterType;

    public bool isGrounded;
    public bool isOnTopOfGolem = false;
    
    [SerializeField] private CharacterMovementController characterMovementController;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _golemLayer;
    [SerializeField] private BoxCollider _collider;

    private Bounds _colliderBounds;
    private float _skinWidth = 0.015f;

    private void Start()
    {
        _colliderBounds = _collider.bounds;
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 _raycastOrigin;
        if (characterType == CharacterType.Golem)
        {
            _raycastOrigin = transform.position + new Vector3(0, _skinWidth, 0);
        }
        else 
        {
            _raycastOrigin = transform.position - (_colliderBounds.size / 2 - new Vector3(0, _skinWidth, 0));
        }

        if ((characterMovementController.golemIsActive && characterType == CharacterType.Golem) ^
            (characterMovementController.mushroomIsActive && characterType == CharacterType.Mushroom))
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

                if (characterType == CharacterType.Mushroom && hit.collider.gameObject.layer == 9)
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