using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ShowTip : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;
    [SerializeField] private GameObject[] _tips;

    private Character _golemCharacter;
    private Character _mushroomCharacter;
    private Renderer _closeTipRenderer;
    private Renderer _mushroomTipRenderer;
    private Renderer _golemTipRenderer;

    private void Start()
    {
        _closeTipRenderer = _tips[0].GetComponent<Renderer>();
        _mushroomTipRenderer = _tips[1].GetComponent<Renderer>();
        _golemTipRenderer = _tips[2].GetComponent<Renderer>();
        _golemCharacter = _golemGameObject.GetComponent<Character>();
        _mushroomCharacter = _mushroomGameObject.GetComponent<Character>();
    }

    private void Update()
    {
        Vector3 dist = _golemGameObject.transform.position - _mushroomGameObject.transform.position;

        _tips[0].transform.position = new Vector3(_golemGameObject.transform.position.x - dist.x/2, _golemGameObject.transform.position.y - dist.y/2 + 5f, 10f);

        if (Mathf.Abs(dist.x) < 2f && Mathf.Abs(dist.y) < 5f && !(_mushroomCharacter.isCombined && _golemCharacter.isCombined))
        {
            _closeTipRenderer.enabled = true;
        }
        else 
        {
            _closeTipRenderer.enabled = false;
        }

        if ((Mathf.Abs(dist.x) >= 2f || Mathf.Abs(dist.y) >= 5f) && !_mushroomCharacter.isActive)
        {
            _mushroomTipRenderer.enabled = true;
        }
        else 
        {
            _mushroomTipRenderer.enabled = false;
        }
        
        if ((Mathf.Abs(dist.x) >= 2f || Mathf.Abs(dist.y) >= 5f) && !_golemCharacter.isActive)
        {
            _golemTipRenderer.enabled = true;
        }
        else
        {
            _golemTipRenderer.enabled = false;
        }
    }
}
