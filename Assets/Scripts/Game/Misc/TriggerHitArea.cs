using UnityEngine;
using System.Collections;

public class TriggerHitArea : MonoBehaviour {

	public GameObject enableObject;

	void OnCollisionEnter2D(Collision2D other){
		if (other != null && other.gameObject.tag == GameHash.HERO_TAG)
		{
			enableObject.SetActive(true);
		}
	}

}
