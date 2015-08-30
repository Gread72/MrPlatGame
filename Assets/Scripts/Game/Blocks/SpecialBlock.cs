using UnityEngine;
using System.Collections;

public class SpecialBlock : MonoBehaviour {

    [SerializeField] private SpriteRenderer _spriteRend;
    [SerializeField] private BoxCollider2D _colliderComp;
    [SerializeField] private ResourceManager _resMgr;
    [SerializeField] private AudioManager _audioMgr;

	public GameObject reward;
    public bool enableHideState = true;
    public int startSpriteIndex = 2;
    public int endSpriteIndex = 4;

	void Awake () {
		_spriteRend = gameObject.GetComponent<SpriteRenderer>();
		_colliderComp = gameObject.GetComponent<BoxCollider2D>();
        _resMgr = gameObject.GetComponent<ResourceManager>();
        _resMgr.currentSprite = startSpriteIndex;

        _audioMgr = AudioManager.GetInstance();

        if (enableHideState) viewableState(false);
		
	}
	
	void OnTriggerEnter2D( Collider2D other ){
		if(other.gameObject.tag == GameHash.HERO_TAG){
            if (enableHideState) viewableState(true);

            _resMgr.currentSprite = endSpriteIndex;
            _colliderComp.isTrigger = false;
            GetComponent<AudioSource>().volume = _audioMgr.SfxAudioVolume;
			GetComponent<AudioSource>().Play();
		}
	}

    void viewableState(bool isHidden){
        _spriteRend.enabled = isHidden;
        reward.SetActive(isHidden);
    }
}
