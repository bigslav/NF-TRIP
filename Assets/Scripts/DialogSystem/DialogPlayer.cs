using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
	public static DialogPlayer instance;

	[SerializeField] private Text textArea;
	[SerializeField] private float defaultLetterDelay;
	[SerializeField] private Image mushroomPhraseWindow;
	[SerializeField] private Image golemPhraseWindow;

	private Queue<Phrase> phrases = new Queue<Phrase>();

	public void PlayDialog(Dialog dialog)
	{
		phrases.Clear();

		Debug.Log("PlayDialog");

		foreach (Phrase phrase in dialog.phrases)
		{
			phrases.Enqueue(phrase);
			Debug.Log("phrase: " + phrase);
		}

		StartCoroutine(TypeNextPhrase(phrases.Dequeue()));
	}

	private IEnumerator TypeNextPhrase(Phrase phrase)
	{
		ShowAuthorsDialogWindow(phrase);

		float letterDelay;
		if (phrase.phraseDuration > 0 && phrase.text.Length > 0)
		{
			letterDelay = phrase.phraseDuration / phrase.text.Length;
		} else
		{
			letterDelay = defaultLetterDelay;
		}


		foreach (char letter in phrase.text.ToCharArray())
		{
			textArea.text += letter;
			yield return new WaitForSeconds(letterDelay);
		}
		
		yield return new WaitForSeconds(phrase.delayAfterPhrase);
		textArea.text = "";
		if (phrases.Count > 0)
		{
			StartCoroutine(TypeNextPhrase(phrases.Dequeue()));
		} else
		{
			EndDialog();
			yield return null;
		}
	}

	void ShowAuthorsDialogWindow(Phrase phrase)
	{
		switch (phrase.author)
		{
			case Phrase.Author.GOLEM:
				mushroomPhraseWindow.gameObject.SetActive(false);
				golemPhraseWindow.gameObject.SetActive(true);
				break;
			case Phrase.Author.MUSHROOM:
				mushroomPhraseWindow.gameObject.SetActive(true);
				golemPhraseWindow.gameObject.SetActive(false);
				break;
		}
	}

	void EndDialog()
	{
		mushroomPhraseWindow.gameObject.SetActive(false);
		golemPhraseWindow.gameObject.SetActive(false);
	}
	
	void Start()
    {
        if (instance == null)
		{
			instance = this;
		}
    }
}
