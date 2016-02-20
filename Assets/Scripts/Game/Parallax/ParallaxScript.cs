using UnityEngine;
using System.Collections;

/*
 * ParallaxScript Class - Utility script for Parallax functionality
 *
 * TODO: 
 * - Change the button state - Restart on the last level 
 * 
 * 
 */

public class ParallaxScript : MonoBehaviour {

    public Player playScript;
    public float offsetValue = 0.25f;

    [SerializeField] private float _currentPlayerXPos = 0f;

	// Use this for initialization
	void Awake () {
		playScript.CurrentRoundedPosition += handleCurrentPosition;
	}

	void handleCurrentPosition(float x, float y){
       if (x > _currentPlayerXPos)
        {
            iTween.MoveBy(gameObject, iTween.Hash("easetype", "easeInSine", "x", offsetValue, "time", 4.0f));
        }
        else
        {
            iTween.MoveBy(gameObject, iTween.Hash("easetype", "easeInSine", "x", -offsetValue, "time", 4.0f));
        }

        _currentPlayerXPos = x;
	}
}
