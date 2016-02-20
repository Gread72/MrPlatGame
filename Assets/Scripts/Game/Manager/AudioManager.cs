using UnityEngine;
using System.Collections;

/*
 * AudioManager Class - Singleton - Audio Manager for most parts of the game - handle volume of audio 
 *
 */

public class AudioManager : MonoBehaviour {

    private static AudioManager _instance;

    //private AudioSource sfxSlimeAudioSource;
    //private AudioSource sfxFlyAudioSource;
    private AudioSource musicBGAudioSource;

    [SerializeField] private float _bgAudioVolume = 0.05f; //0.05f;
    public float BgAudioVolume
    {
        get { return _bgAudioVolume; }
        set { 
        _bgAudioVolume = value;
        musicBGAudioSource.volume = _bgAudioVolume;
        }
    }

    [SerializeField] private float _sfxAudioVolume = 0.05f;
    public float SfxAudioVolume
    {
        get { return _sfxAudioVolume; }
        set { _sfxAudioVolume = value; }
    }

    public AudioClip walkingAudio;
    public AudioClip jumpingAudio;
    public AudioClip pickUpAudio;
    public AudioClip hitSound;
    public AudioClip slimeEnemySound;
    public AudioClip flyEnemySound;
    public AudioClip backgroundMusic;

    public enum SOUNDS
    {
        WALKING_AUDIO,
        JUMPING_AUDIO,
        PICKUP_AUDIO,
        HIT_AUDIO,
        SLIME_ENEMY_AUDIO,
        FLY_ENEMY_AUDIO
    }

    // Use this for initialization
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance) Destroy(_instance);
        }

        musicBGAudioSource = gameObject.AddComponent<AudioSource>();
        //sfxSlimeAudioSource.volume = _sfxAudioVolume;
        //sfxFlyAudioSource.volume = _sfxAudioVolume;
        //sfxSlimeAudioSource = gameObject.AddComponent<AudioSource>();
        //sfxFlyAudioSource = gameObject.AddComponent<AudioSource>();

        PlayBackgroundMusic();
    }

    static public AudioManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<AudioManager>();
            if (_instance == null)
            {
                Debug.Log("Something went wrong.");
            }
            DontDestroyOnLoad(_instance);
        }
        return _instance;
    }

    public void PlayOneShotAudio(AudioManager.SOUNDS soundID)
    {
        Debug.Log("soundID " + soundID);

        switch (soundID)
        {
            case AudioManager.SOUNDS.WALKING_AUDIO:
                GetComponent<AudioSource>().PlayOneShot(walkingAudio, _sfxAudioVolume);
                break;

            case AudioManager.SOUNDS.JUMPING_AUDIO:
                GetComponent<AudioSource>().PlayOneShot(jumpingAudio, _sfxAudioVolume);
                break;

            case AudioManager.SOUNDS.PICKUP_AUDIO:
                GetComponent<AudioSource>().PlayOneShot(pickUpAudio, _sfxAudioVolume);
                break;
        }
        
    }

    //public void PlayEnemySFX(AudioManager.SOUNDS soundID)
    //{
        
    //    switch (soundID)
    //    {
    //        case SOUNDS.SLIME_ENEMY_AUDIO:
    //           if (sfxSlimeAudioSource.clip == null)
    //            {
    //                sfxSlimeAudioSource.clip = slimeEnemySound;
    //            }
    //            if (sfxSlimeAudioSource.isPlaying) return;
    //            sfxSlimeAudioSource.Play();
    //            break;

    //        case SOUNDS.FLY_ENEMY_AUDIO:
    //            if (sfxFlyAudioSource.clip == null)
    //            {
    //                sfxFlyAudioSource.clip = flyEnemySound;
                    
    //            }
    //            if (sfxFlyAudioSource.isPlaying) return;
                
    //            sfxFlyAudioSource.Play();
    //            break;
    //    }
    //}

    public void PlayBackgroundMusic()
    {
        if (ReferenceEquals(musicBGAudioSource.clip, null)) musicBGAudioSource.clip = backgroundMusic;
        musicBGAudioSource.loop = true;
        musicBGAudioSource.volume = _bgAudioVolume;
        musicBGAudioSource.Play();
    }

}
