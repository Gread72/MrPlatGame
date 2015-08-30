using UnityEngine;
using System.Collections;

public class SwitchMediator : MonoBehaviour {

	public GameObject[] wallBlocks;
	public Sprite switchOnSprite;
	public Sprite switchOffSprite;

    [SerializeField] private AudioManager _audioMgr;
    [SerializeField] private bool isSwitchOn = false;
    
    void Awake()
    {
        _audioMgr = AudioManager.GetInstance();
    }

	void OnTriggerEnter2D(Collider2D other){ //hero hits side of enemy

        if (GetComponent<AudioSource>() && isSwitchOn == false)
        {
            GetComponent<AudioSource>().volume = _audioMgr.SfxAudioVolume;
            GetComponent<AudioSource>().Play();
        }
		if (other != null && other.gameObject.tag == GameHash.HERO_TAG && isSwitchOn == false)
		{
			isSwitchOn = true;
			SpriteRenderer spriteRender = gameObject.GetComponent<SpriteRenderer>();
			spriteRender.sprite = switchOnSprite;
			int multiplyPos = 1;
			foreach(GameObject item in wallBlocks){
				item.AddComponent<Rigidbody2D>();
				item.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 100 * multiplyPos));
				item.GetComponent<Collider2D>().enabled = false;
				multiplyPos = multiplyPos + 1;
			}
		}
	}
}
