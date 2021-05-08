using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoveTip : MonoBehaviour
{
    [SerializeField] private LayerMask whoCanInteract;

    [SerializeField] private GameObject pressETip;
    [SerializeField] private Sprite pressERed;
    [SerializeField] private Sprite pressEGreen;

    private SpriteRenderer _tipRenderer;

    private void Start()
    {
        _tipRenderer = pressETip.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (whoCanInteract == (whoCanInteract | (1 << collision.gameObject.layer)))
        {
            _tipRenderer.sprite = pressEGreen;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (whoCanInteract == (whoCanInteract | (1 << collision.gameObject.layer)))
        {
            _tipRenderer.sprite = pressERed;
        }
    }
}
