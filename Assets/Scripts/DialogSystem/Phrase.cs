using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase : MonoBehaviour
{
	public enum Author { GOLEM, MUSHROOM}

	public Author author;
	[TextArea (3, 10)]
	public string text; 
}
