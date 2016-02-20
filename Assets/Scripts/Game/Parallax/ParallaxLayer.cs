using UnityEngine;
using System.Collections;

/*
 * ParallaxLayer Class - Utility script for Parallax functionality - editor to set parallax
 *
 * 
 * 
 */

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour {
	public float speedX;
	public float speedY;
	public bool moveInOppositeDirection;

	[SerializeField] private Transform _cameraTransform;
    [SerializeField] private Vector3 _previousCameraPosition;
    [SerializeField] private bool _previousMoveParallax;
    [SerializeField] private ParallaxOption _options;

	void OnEnable() {
		GameObject gameCamera = GameObject.Find(GameHash.MAIN_CAMERA_G_OBJ_NAME);
		_options = gameCamera.GetComponent<ParallaxOption>();
		_cameraTransform = gameCamera.transform;
		_previousCameraPosition = _cameraTransform.position;
	}

	void Update () {
		if(_options.moveParallax && !_previousMoveParallax)
			_previousCameraPosition = _cameraTransform.position;

		_previousMoveParallax = _options.moveParallax;

		if(!Application.isPlaying && !_options.moveParallax)
			return;

		Vector3 distance = _cameraTransform.position - _previousCameraPosition;
		float direction = (moveInOppositeDirection) ? -1f : 1f;
		transform.position += Vector3.Scale(distance, new Vector3(speedX, speedY)) * direction;

		_previousCameraPosition = _cameraTransform.position;
	}
}
