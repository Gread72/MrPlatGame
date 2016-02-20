using UnityEngine;
using System.Collections;

/*
 * ParallaxOption Class - Utility script for Parallax functionality - save/restore position
 *
 * 
 * 
 */

public class ParallaxOption : MonoBehaviour {

	public bool moveParallax;

	[SerializeField]
	[HideInInspector]
	private Vector3 storedPosition;

	public void SavePosition() {
		storedPosition = transform.position;
	}

	public void RestorePosition() {
		transform.position = storedPosition;
	}
}