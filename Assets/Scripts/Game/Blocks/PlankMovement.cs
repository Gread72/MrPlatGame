using UnityEngine;
using System.Collections;

/*
 * PlankMovement Class - Mediator for Plank Component
 *
 */

public class PlankMovement : MonoBehaviour {

    public direction currentDirection;
	public Vector2[] boundaryParams = new Vector2[2];
    public float moveSpeed = 1.0f;
    public BoundaryParamVO boundParam;

	public enum direction{
		top = 0,
		bottom = 1
	};
	
	// Update is called once per frame
	void Update () {
        if (currentDirection == direction.top && transform.position.y > boundParam.BottomRight.y)
        {
			transform.position = new Vector3( transform.position.x , 
			                                 transform.position.y + (Time.deltaTime * moveSpeed), 
			                                 transform.position.z); //Move the enemy to the left
		}else{
			currentDirection = direction.bottom;
		}

        if (currentDirection == direction.bottom && transform.position.y < boundParam.TopLeft.y)
        {
			transform.position = new Vector3( transform.position.x , 
			                                 transform.position.y + (Time.deltaTime * -moveSpeed), 
			                                 transform.position.z); //Move the enemy to the left
		}else{
			currentDirection = direction.top;
		}

	}
}
