using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPlayer : MonoBehaviour
{
	public static DialogPlayer instance;

	[SerializeField] private Text textArea;
	[SerializeField] private float letterDelay;
	[SerializeField] private float phraseDelay;
	[SerializeField] private Image GolemPhraseWindow;
	[SerializeField] private Image MushroomPhraseWindow;

	private Queue<Phrase> phrases = new Queue<Phrase>();

	public void PlayDialog(Dialog dialog)
	{
		phrases.Clear();

		foreach (Phrase phrase in dialog.phrases)
		{
			phrases.Enqueue(phrase);
		}

		StartCoroutine(TypeNextPhrase(phrases.Dequeue()));
	}

	private IEnumerator TypeNextPhrase(Phrase phrase)
	{
		ShowAuthorsDialogWindow(phrase);

		foreach (char letter in phrase.text.ToCharArray())
		{
			textArea.text += letter;
			yield return new WaitForSeconds(letterDelay);
		}
		
		yield return new WaitForSeconds(phraseDelay);
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
				MushroomPhraseWindow.gameObject.SetActive(false);
				GolemPhraseWindow.gameObject.SetActive(true);
				break;
			case Phrase.Author.MUSHROOM:
				MushroomPhraseWindow.gameObject.SetActive(true);
				GolemPhraseWindow.gameObject.SetActive(false);
				break;
		}
	}

	void EndDialog()
	{
		MushroomPhraseWindow.gameObject.SetActive(false);
		GolemPhraseWindow.gameObject.SetActive(false);
	}
	
	void Start()
    {
        if (instance == null)
		{
			instance = this;
		}
    }
}
