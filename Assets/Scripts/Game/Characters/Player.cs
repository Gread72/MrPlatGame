using UnityEngine;
using System.Collections;
using System;

/*
 * Player Class - All things to do with the player game object, a mediator 
 *
 */

public delegate void DelegateCurrentRoundedPosition(float x, float y);
public delegate void GoalFoundEvent();

public class Player : MonoBehaviour {

	public float speedX = 10f;
	public float speedY = 150f;
	public AudioClip walkingAudio;
	public AudioClip jumpingAudio;
	public AudioClip pickUpAudio;

    [HideInInspector]
    public bool hasJumped = false;
    public ArrayList keys;
    public DelegateCurrentRoundedPosition CurrentRoundedPosition;
    public GoalFoundEvent GoalFoundEvent;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private bool _isMovingRight = false;
    [SerializeField]
    private bool _isMovingLeft = false;
    //[SerializeField]
    //private AudioSource _audioSource;
    [SerializeField]
    private GameObject _bridge;
    [SerializeField]
    private bool _isOnBridge = false;
    [SerializeField]
    private GameController _gameController;
    [SerializeField]
    private AudioManager _audioMgr;

	void Awake(){
		_gameController = FindObjectOfType<GameController>();
		_animator = GetComponent<Animator>(); 
		keys = new ArrayList();

        //_audioSource = GetComponent<AudioSource>();	
        _audioMgr = AudioManager.GetInstance();
	}

	// Update is called once per frame
	void Update () {

		KeyboardControl();

		if(_isMovingRight == true){
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * speedX);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
			//audio.PlayOneShot(walkingAudio);
            //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.WALKING_AUDIO);
		}

		if(_isMovingLeft == true){
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.left * speedX);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
			//audio.PlayOneShot(walkingAudio);
            //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.WALKING_AUDIO);
		}

		if(_isOnBridge == true && _bridge != null){
            this.gameObject.transform.position = new Vector3(_bridge.transform.position.x,
                                                             this.gameObject.transform.position.y,
                                                             this.gameObject.transform.position.z);
		}

	}

	void KeyboardControl(){
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			_isMovingRight = true;
            _animator.SetInteger(GameHash.ANIMSTATE_ANIM, 1);
		}else if(Input.GetKeyDown(KeyCode.LeftArrow)){
			_isMovingLeft = true;
			_animator.SetInteger(GameHash.ANIMSTATE_ANIM, 1);
		}else if(Input.GetKeyDown(KeyCode.UpArrow) && hasJumped == false){
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * speedY);
			_animator.SetInteger(GameHash.ANIMSTATE_ANIM, 2);
			//audio.PlayOneShot(jumpingAudio);
            //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.JUMPING_AUDIO);
			hasJumped = true;
			_isOnBridge = false;
		}else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow)){
			_isMovingRight = false;
			_isMovingLeft = false;
			_animator.SetInteger(GameHash.ANIMSTATE_ANIM, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D other) //Hero jumps on enemy
	{
		if (other != null && other.gameObject.tag == GameHash.FLOOR_GROUND_TAG)
		{
			if(other.gameObject.name == GameHash.MOVING_BRIDGE_G_OBJ_NAME){
				_bridge = other.gameObject;
				_isOnBridge = true;
			}else{
				_bridge = null;
				_isOnBridge = false;
			}

			hasJumped = false;
			if(CurrentRoundedPosition != null){
				CurrentRoundedPosition(Mathf.Round(gameObject.transform.position.x), Mathf.Round(gameObject.transform.position.y));
			}
	    }else{
			if(other != null && System.Text.RegularExpressions.Regex.IsMatch(other.gameObject.name, "key_")){
				_gameController.KeyFound(other.gameObject.name);
				keys.Add(other.gameObject.name);
				Destroy(other.gameObject);
                GetComponent<AudioSource>().PlayOneShot(pickUpAudio, _audioMgr.SfxAudioVolume);
                //audioMgr.PlayOneShotAudio(AudioManager.SOUNDS.PICKUP_AUDIO);
			}

			if(other.gameObject.tag == GameHash.GOAL_TAG){
				GoalFoundEvent();
			}
		}
	}

}
