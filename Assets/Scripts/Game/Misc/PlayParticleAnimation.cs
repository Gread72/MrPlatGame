using UnityEngine;
using System.Collections;

public class PlayParticleAnimation : MonoBehaviour {

	[SerializeField] private ParticleSystem _anim;

	// Use this for initialization
	void Start () {
		_anim = GameObject.FindObjectOfType<ParticleSystem>(); //ParticleSystem;
		_anim.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
