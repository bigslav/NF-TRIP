using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Shadows2D : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _shadowsProcessingSetUp = false;

    private void OnRenderObject()
    {
        if (!_shadowsProcessingSetUp)
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.receiveShadows = true;
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            _shadowsProcessingSetUp = true;
        }
    }
}
