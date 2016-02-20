using UnityEngine;
using System.Collections;

/*
 * CloudManager Class - Handle Parallax for clouds
 *
 */

public class CloudManager : MonoBehaviour {

	public GameObject[] clouds;
    public float offsetValue = 0.25f;
    private Player playScript;

	[SerializeField] private float currentPlayerXPos = 0f;

	// Use this for initialization
	void Awake () {
        playScript = GameObject.FindObjectOfType<Player>();
		playScript.CurrentRoundedPosition += handleCurrentPosition;

	}

	void handleCurrentPosition(float x, float y){
		float moveByPosition_1;
		float moveByPosition_2;
		float moveByPosition_3;

		if(currentPlayerXPos != x){
			if(x > currentPlayerXPos){
                moveByPosition_1 = offsetValue * 3;
                moveByPosition_2 = offsetValue * 2;
                moveByPosition_3 = offsetValue;
			}else{
                moveByPosition_1 = -offsetValue * 3; // 1
                moveByPosition_2 = -offsetValue * 2; //0.5
                moveByPosition_3 = -offsetValue; //.25
			}
			int cloudIndex = 1;
			foreach(GameObject item in clouds){
				switch(cloudIndex){
				case 1:
					iTween.MoveBy( item,iTween.Hash("easetype","easeInSine", "x", moveByPosition_1, "time", 8.0f) );
					break;
				case 2:
					iTween.MoveBy( item,iTween.Hash("easetype","easeInSine", "x", moveByPosition_2, "time", 4.0f) );
					break;
				case 3:
					iTween.MoveBy( item,iTween.Hash("easetype","easeInSine", "x", moveByPosition_3, "time", 8.0f) );
					break;
				}
				cloudIndex++;
			}

			currentPlayerXPos = x;
		}
	}
}
