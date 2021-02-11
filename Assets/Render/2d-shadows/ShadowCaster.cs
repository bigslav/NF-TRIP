using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class ShadowCaster : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
