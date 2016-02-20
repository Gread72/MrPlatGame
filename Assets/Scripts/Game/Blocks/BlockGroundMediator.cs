using UnityEngine;
using System.Collections;

/*
 * BlockGroundMediator Class - Mediator for Block Ground Component
 *
 */

public class BlockGroundMediator : MonoBehaviour {

	public GameObject block;
	public Player playScript;

    [SerializeField] private int _gameAreaWidth;
    [SerializeField] private int _gameAreaHeight;
    [SerializeField] private int _numOfTilesWidth;
    [SerializeField] private ArrayList _blockList;
    [SerializeField] private int _currentXPosition;

	// Use this for initialization
	void Start () {
		_currentXPosition = 0;

		_gameAreaWidth = Screen.width;
		_gameAreaHeight = Screen.height;
		_numOfTilesWidth = _gameAreaWidth/70;

		if(_numOfTilesWidth % 2 != 0){
			_numOfTilesWidth = _numOfTilesWidth + 1;
		}

		_numOfTilesWidth = _numOfTilesWidth + 2;

		_blockList = new ArrayList((_numOfTilesWidth * 2)+1);
		int iterator = 0;
		for(int count = -_numOfTilesWidth; count <= _numOfTilesWidth; count++){
			_blockList.Add( (GameObject)Instantiate(block, new Vector3(count, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity) );
			iterator++;
		}

		Debug.Log(_blockList.Count);
	}

	GameObject AddBlock(float xPos){
		GameObject createBlock = (GameObject)Instantiate(block, new Vector3(xPos, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
		return createBlock;
	}
	
}
