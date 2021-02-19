using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Vector3 cameraOffset;
	[SerializeField] private Transform firstFollowedCharacter;
	[SerializeField] private Transform secondFollowedCharacter;
	[SerializeField] private float minCameraFieldOfView;
	[SerializeField] private float maxCameraFieldOfView;
	[SerializeField] private Camera camera;

	[SerializeField] private Transform bottomObject;
	[SerializeField] private Transform topObject;
	[SerializeField] private Transform leftObject;
	[SerializeField] private Transform rightObject;

	[SerializeField] private float cameraLerpValue;
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
}
