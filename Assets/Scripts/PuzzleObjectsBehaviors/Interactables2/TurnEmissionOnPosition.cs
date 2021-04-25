using UnityEngine;

public class TurnEmissionOnPosition : MonoBehaviour
{
    public MeshRenderer targetMeshRenderer;
    public Transform turnEmissionOnPos;
    public Transform turnEmissionOffPos;

    private Material _mat;
    private Color _col;

    private void Start()
    {
        _mat = targetMeshRenderer.material;
        _col = _mat.GetColor("_EmissionColor");
    }

    private void Update()
    {
        if (transform.position == turnEmissionOnPos.position)
        {
            _mat.EnableKeyword("_EMISSION");
            _mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
            _mat.SetColor("_EmissionColor", _col);
        }
        else if (transform.position == turnEmissionOffPos.position) 
        {
            _mat.DisableKeyword("_EMISSION");
            _mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            _mat.SetColor("_EmissionColor", Color.black);
        }
    }
}
