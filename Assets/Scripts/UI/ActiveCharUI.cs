using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveCharUI : MonoBehaviour
{
	[SerializeField] private Image faceMushroom;
	[SerializeField] private Image faceGolem;
	private float scaleBig = 1.33f;
	private float scaleSmall = 0.75f;
	private float posDeltaBig = 25;
	private float posDeltaSmall = 20;
	bool activeChar = false;

	private Vector3 defaultMushroomFacePosition;
	private Vector3 defaultGolemFacePosition;

	private void Start()
	{
		defaultMushroomFacePosition = faceMushroom.transform.localPosition;
		defaultGolemFacePosition = faceGolem.transform.localPosition;

		//StartCoroutine(go());
		//faceMushroom.transform.localScale = new Vector3(1.33f, 1.33f, 1);
		//faceMushroom.transform.localPosition += new Vector3(25, 25, 0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			StopAllCoroutines();
			if (activeChar)
			{
				ActivateMushroom();
			} else
			{
				ActivateGolem();
			}
			activeChar = !activeChar;
		}
	}

	public IEnumerator GrowFace(Image image, float scale, Vector3 newPosition)
	{
		Vector3 oldScale = image.transform.localScale;
		Vector3 newScale = new Vector3(scale, scale, 1);
		Vector3 oldPosition = image.transform.localPosition;

		for (float i = 0; i < 1; i += Time.deltaTime / 0.5f)
		{
			image.transform.localScale = Vector3.Lerp(oldScale, newScale, EasingSmoothSquared(i));
			image.transform.localPosition = Vector3.Lerp(oldPosition, newPosition, EasingSmoothSquared(i));

			yield return null;
		}

		image.transform.localScale = newScale;
		image.transform.localPosition = newPosition;
	}

	public void ActivateMushroom()
	{
		StartCoroutine(GrowFace(faceMushroom, scaleBig, defaultMushroomFacePosition + new Vector3(posDeltaBig, -posDeltaBig, 0)));
		StartCoroutine(GrowFace(faceGolem, scaleSmall, defaultGolemFacePosition + new Vector3(-posDeltaSmall, -posDeltaSmall, 0)));
	}

	public void ActivateGolem()
	{
		StartCoroutine(GrowFace(faceMushroom, scaleSmall, defaultMushroomFacePosition + new Vector3(-posDeltaSmall, posDeltaSmall, 0)));
		StartCoroutine(GrowFace(faceGolem, scaleBig, defaultGolemFacePosition + new Vector3(posDeltaBig, posDeltaBig, 0)));
	}

	public void ActivateGolemAndMushroom()
	{
		StartCoroutine(GrowFace(faceMushroom, 1f, defaultMushroomFacePosition));
		StartCoroutine(GrowFace(faceGolem, 1f, defaultGolemFacePosition));
	}

	private float EasingSmoothSquared(float x)
	{
		return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
	}

	private IEnumerator go()
	{
		yield return new WaitForSeconds(1.5f);

		ActivateGolem();

		yield return new WaitForSeconds(1.5f);

		ActivateMushroom();

		yield return new WaitForSeconds(1.5f);

		ActivateGolemAndMushroom();
	}
}
