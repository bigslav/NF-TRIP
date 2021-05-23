using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phrase : MonoBehaviour
{
	public enum Author { GOLEM, MUSHROOM}

	public Author author;
	public float delayAfterPhrase;
	public float phraseDuration;
	[TextArea (3, 10)]
	public string text; 
}
