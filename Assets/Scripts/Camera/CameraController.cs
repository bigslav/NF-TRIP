using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private CameraMode cameraMode;
	
	[Header("Static camera settings")]
	[SerializeField] private float puzzleFieldOfView;
	[SerializeField] private float firstCaveFieldOfView;
	[SerializeField] private float secondCaveFieldOfView;
	[SerializeField] private float firstCaveYAxis;
	[SerializeField] private float secondCaveYAxis;
	[SerializeField] private Vector3 forwardOffset;

	[Header("Dynamic camera settings")]
	[SerializeField] private float minCameraFieldOfView;
	[SerializeField] private float maxCameraFieldOfView;
	[SerializeField] private float cameraLerpValue;


	[Header("Links")]
	[SerializeField] private Camera camera;
	[SerializeField] private Transform bottomObject;
	[SerializeField] private Transform topObject;
	[SerializeField] private Transform leftObject;
	[SerializeField] private Transform rightObject;

	[Header("General Settings")]
	[SerializeField] private Vector3 cameraOffset;
	[SerializeField] private Transform firstFollowedCharacter;
	[SerializeField] private Transform secondFollowedCharacter;
	[SerializeField] private float cameraSmoothness;

	private Vector3 levelCenterPosition;

	private void Start()
	{
		levelCenterPosition.y = 0.5f * (bottomObject.position.y + topObject.position.y);
		levelCenterPosition.x = 0.5f * (leftObject.position.x + rightObject.position.x);
		levelCenterPosition.z = 0.25f * (bottomObject.position.z + topObject.position.z + leftObject.position.z + rightObject.position.z);
	}

	private void LateUpdate()
	{
		if (cameraMode == CameraMode.Dynamic)
		{
			DynamicCamera();
		} else if (cameraMode == CameraMode.Static)
		{
			StaticCamera();
		}
	}

	private void StaticCamera()
	{
		Vector3 positionBetweenChars = 0.5f * (firstFollowedCharacter.position + secondFollowedCharacter.position);
		Vector3 newCameraPosition = positionBetweenChars + cameraOffset;
		float customCameraSmoothness;
		if (positionBetweenChars.x < leftObject.position.x)
		{
			customCameraSmoothness = cameraSmoothness * 0.1f;
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, firstCaveFieldOfView, customCameraSmoothness);

			newCameraPosition.y = firstCaveYAxis;
			newCameraPosition.x += forwardOffset.x;
		} else if (positionBetweenChars.x > rightObject.position.x)
		{
			customCameraSmoothness = cameraSmoothness * 0.1f;
			customCameraSmoothness = cameraSmoothness * 0.1f;
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, secondCaveFieldOfView, customCameraSmoothness);

			newCameraPosition.y = secondCaveYAxis;
			newCameraPosition.x += forwardOffset.x;
		} else
		{
			customCameraSmoothness = cameraSmoothness * 0.01f;
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, puzzleFieldOfView, customCameraSmoothness);
			
			newCameraPosition.x = levelCenterPosition.x;
			if (newCameraPosition.y < 11)
			{
				newCameraPosition.y = 11;
			}
			if (newCameraPosition.y > 44)
			{
				newCameraPosition.y = 44;
			}
		}
		transform.position = Vector3.Lerp(transform.position, newCameraPosition, customCameraSmoothness);
	}

	private void DynamicCamera()
	{
		Vector3 oldPosition = transform.position;

		Vector3 positionBetweenChars = 0.5f * (firstFollowedCharacter.position + secondFollowedCharacter.position);
		Vector3 newPosition = Vector3.Lerp(positionBetweenChars, levelCenterPosition, cameraLerpValue) + cameraOffset;

		transform.position = Vector3.Lerp(oldPosition, newPosition, Time.deltaTime * cameraSmoothness);

		Vector3 distanceBetweenVector = firstFollowedCharacter.position - secondFollowedCharacter.position;
		distanceBetweenVector.y *= 2;
		float distanceBetween = distanceBetweenVector.magnitude;
		float oldFieldOfView = camera.fieldOfView;
		float newFieldOfView = Mathf.Clamp(distanceBetween * 1.5f, minCameraFieldOfView, maxCameraFieldOfView);
		camera.fieldOfView = Mathf.Lerp(oldFieldOfView, newFieldOfView, Time.deltaTime * cameraSmoothness);
	}

	enum CameraMode
	{
		Dynamic,
		Static
	}
}
