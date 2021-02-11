using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class ShadowCaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().castShadows = true;
		GetComponent<SpriteRenderer>().receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
