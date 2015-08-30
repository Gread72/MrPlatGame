using UnityEngine;
using System.Collections;

public class CameraMediator : MonoBehaviour {

	public GameObject target;
    public float clampXPosMin = 6.42f;
    public float clampXPosMax = 87.69f;
    public float clampYPosMin = -1.6f;
    public float clampYPosMax = -0.23f;

    [SerializeField] private float _xPos = 0f;
    [SerializeField] private float _yPos = 0f;
	
	// Update is called once per frame
	void Update () {
		if(target != null){

           _xPos = Mathf.Clamp(target.transform.position.x, clampXPosMin, clampXPosMax);
           _yPos = Mathf.Clamp(target.transform.position.y, clampYPosMin, clampYPosMax);

           Vector3 setPosition = new Vector3(_xPos,_yPos, Camera.main.transform.position.z); 

           Camera.main.transform.position =  iTween.Vector3Update(Camera.main.transform.position, setPosition, 2f);

		}
	}
}
