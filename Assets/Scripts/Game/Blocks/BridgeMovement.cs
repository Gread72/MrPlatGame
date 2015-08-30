using UnityEngine;
using System.Collections;

public class BridgeMovement : MonoBehaviour {

    public int currentDirection;
	public Vector2[] boundaryParams = new Vector2[2];
    public BoundaryParamVO boundParam;
    public float moveSpeed = 1.0f;

	public enum direction{
		left = 0,
		right = 1
	};

	// Update is called once per frame
	void Update () {
        if (currentDirection == (int)direction.left && transform.position.x > boundParam.TopLeft.x)
        {
			transform.position = new Vector3( transform.position.x + (Time.deltaTime * -moveSpeed), 
			                                 transform.position.y, 
			                                 transform.position.z); //Move the enemy to the left
		}else{
			currentDirection = (int)direction.right;
		}

        if (currentDirection == (int)direction.right && transform.position.x < boundParam.BottomRight.x)
        {
			transform.position = new Vector3( transform.position.x + (Time.deltaTime * (moveSpeed) ), 
			                                 transform.position.y, 
			                                 transform.position.z); //Move the enemy to the left
		}else{
			currentDirection = (int)direction.left;
		}
	}
}
