using UnityEngine;
using System.Collections;

/*
 * PhysicExplode Class - Apply force to explode 
 *
 */

public class PhysicExplode : MonoBehaviour {

	public float xDirection = 20;
	public float yDirection = 120;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xDirection, yDirection)); //Vector2.up * 100
	}
	
}
