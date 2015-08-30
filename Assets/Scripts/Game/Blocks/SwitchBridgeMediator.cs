using UnityEngine;
using System.Collections;

public class SwitchBridgeMediator : MonoBehaviour
{
    public Sprite switchOnSprite;
    public Sprite switchOffSprite;
    public BridgeMovement bridgeMove;
    public int bridgeSpeed = 1;

    [SerializeField] private bool isSwitchOn = false;
    [SerializeField] private AudioManager _audioMgr;

    void Awake()
    {
        _audioMgr = AudioManager.GetInstance();
    }

    void OnTriggerEnter2D(Collider2D other)
    { //hero hits side of enemy

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
            bridgeMove.moveSpeed = bridgeSpeed;
        }
    }
}
