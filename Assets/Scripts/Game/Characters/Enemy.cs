using UnityEngine;
using System.Collections;
using System;

/*
 * Enemy Class - All things to do with the enemy game object, a mediator 
 *
 */

[Serializable]
public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public AudioClip hitSound;
    public Vector2[] boundaryParams = new Vector2[2];
    public BoundaryParamVO boundParam;
    public enum direction{
		left = 0,
		right = 1
	};
    public direction currentDirection;

    [SerializeField] private GameObject _gameManager;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _hasDied = false; /// either enemy or player died - kill switch
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameModel _refGameModel;
    [SerializeField] private AudioManager _audioMgr;

    void Awake()
    {
        _refGameModel = GameModel.GetInstance();
        _audioMgr = AudioManager.GetInstance();
    }

	// Use this for initialization
	void Start(){
        _gameManager = GameObject.FindGameObjectWithTag(GameHash.GAME_MANAGER_TAG);
		_gameController = _gameManager.GetComponent<GameController>();

		_anim = gameObject.GetComponent<Animator>();
        _anim.SetBool(GameHash.MOVING_ANIM, true);

		_audioSource = gameObject.GetComponent<AudioSource>();
       
	}
	
	// Update is called once per frame
	void Update(){

        _audioSource.volume = _audioMgr.SfxAudioVolume;

		if(_hasDied == true) return;
        // element 1 - left edge
        if (currentDirection == direction.left && transform.position.x > boundParam.TopLeft.x)
        {
			transform.position = new Vector3( transform.position.x + (Time.deltaTime * moveSpeed), 
			                                 transform.position.y, 
			                                 transform.position.z); //Move the enemy to the left
			transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
		}else{
			currentDirection = direction.right;
		}
        // element 2 - right edge
		if( currentDirection == direction.right && transform.position.x < boundParam.BottomRight.x){
			transform.position = new Vector3( transform.position.x + (Time.deltaTime * -(moveSpeed) ), 
			                                 transform.position.y, 
			                                 transform.position.z); //Move the enemy to the left
			transform.localScale = new Vector3(-1.4f, 1.4f, 1.4f);
		}else{
			currentDirection = (int)direction.left;
		}

		if(!_audioSource.isPlaying) _audioSource.Play();
       
	}

	void ChangeVectorProperty(Vector3 source, float xPos = 0f, float yPos = 0f, float zPos = 0f){
		if(source != null){
			source = new Vector3(xPos, yPos, zPos);
		}

	}
	
	void OnTriggerEnter2D(Collider2D other) //hero hits side of enemy
	{
		if(_hasDied == true) return;

		if (other.gameObject.tag == GameHash.HERO_TAG)
		{
			if(_gameController.playScript.hasJumped == false){
                _hasDied = true;
				_gameController.PlayerLoses(gameObject.transform.position);
                AudioSource.PlayClipAtPoint(hitSound, transform.position, _audioMgr.SfxAudioVolume);
               //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.HIT_AUDIO);
				Destroy(other.gameObject); //Remove collider to avoid audio replaying
			}else{
                _refGameModel.AddToEnemyList("Enemy_01");
                AudioSource.PlayClipAtPoint(hitSound, transform.position, _audioMgr.SfxAudioVolume);
                //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.HIT_AUDIO);
				_anim.SetBool(GameHash.DIED_ANIM, true);
				_hasDied = true;
				gameObject.AddComponent<Rigidbody2D>();
				StartCoroutine(Destroy());
			}

		}
	}

	IEnumerator Destroy(){
		yield return new WaitForSeconds(5);
		Destroy(gameObject);
	}
}