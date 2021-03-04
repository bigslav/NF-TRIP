using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTip : MonoBehaviour
{
    [SerializeField] private GameObject _golemGameObject;
    [SerializeField] private GameObject _mushroomGameObject;

    [SerializeField] private GameObject _tip;

    private void Update()
    {
        Vector3 dist = _golemGameObject.transform.position - _mushroomGameObject.transform.position;

        if (Mathf.Abs(dist.x) < 2f && Mathf.Abs(dist.y) < 5f)
        {
            _tip.SetActive(true);
        }
        else 
        {
            _tip.SetActive(false);
        }
    }
}
