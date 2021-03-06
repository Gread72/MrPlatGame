﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * UIMediator Class - Mediator for the canvas UI components 
 *
 * 
 */

public class UIMediator : MonoBehaviour {
    
    public GameObject configPanelWindow;
    public GameObject endOfRoundWindow;
    public Button nextLevelButton;
    public Button replayButton;
    public Text StatusText;
    public Button gearButton;
    public Slider bgAudioVolSlider;
    public Slider sfxAudioVolSlider;

    private const string KEY_RED = "key_red";
    private const string KEY_GREEN = "key_green";
    private const string KEY_BLUE = "key_blue";
    private const string KEY_YELLOW = "key_yellow";

    private const string IMAGE_KEY_RED = "red_key";
    private const string IMAGE_KEY_GREEN = "green_key";
    private const string IMAGE_KEY_BLUE = "blue_key";
    private const string IMAGE_KEY_YELLOW = "yellow_key";
    private const string IMAGE_PLAYER_COUNT_1 = "player_count_1";
    private const string IMAGE_PLAYER_COUNT_2 = "player_count_2";
    private const string IMAGE_PLAYER_COUNT_3 = "player_count_3";


    [HideInInspector][SerializeField] private Button _goToMenuButton;
    [HideInInspector][SerializeField] private Button _nextButton;

    [HideInInspector][SerializeField] private Image _greenKey;

    [HideInInspector]
    [SerializeField]
    private Image _blueKey;

    [HideInInspector]
    [SerializeField]
    private Image _yellowKey;

    [HideInInspector]
    [SerializeField]
    private Image _redKey;

    [HideInInspector]
    [SerializeField]
    private Image _playerLives1;

    [HideInInspector]
    [SerializeField]
    private Image _playerLives2;

    [HideInInspector]
    [SerializeField]
    private Image _playerLives3;

    [HideInInspector]
    [SerializeField]
    private bool _isPopUpConfigWindowOpen = false;

    [HideInInspector]
    [SerializeField]
    private GameController _gameController;

    [HideInInspector]
    [SerializeField]
    private Image[] _currentImages;

    [HideInInspector]
    [SerializeField]
    private AudioManager _audioMgr;

	// Use this for initialization
	void Awake () {
		_gameController = FindObjectOfType<GameController>();

        _audioMgr = AudioManager.GetInstance();

        setupImages();

        setupLivesDisplay();

        GameModel.GetInstance().PlayerStatusUpdateEvent += new GameModel.PlayerStatusUpdate(playerDisplayUpdate);

        configPanelWindow.SetActive(false);

        gearButton.onClick.AddListener(() => { PopUpConfigWindowViewToggle(); });

        endOfRoundWindow.SetActive(false);

        nextLevelButton.onClick.AddListener(() => {
            _gameController.NavToNext(); 
        });

        replayButton.onClick.AddListener(() => { _gameController.ReplayRound(); });

        bgAudioVolSlider.value = _audioMgr.BgAudioVolume;

        sfxAudioVolSlider.value = _audioMgr.SfxAudioVolume;

	}

    private void playerDisplayUpdate()
    {
        setupLivesDisplay();
    }

	public void EnableKeyFound(string keyname){
		switch(keyname){
		case KEY_RED:
			_redKey.gameObject.SetActive(true);
			break;
		case KEY_GREEN:
			_greenKey.gameObject.SetActive(true);
			break;
		case KEY_BLUE:
			_blueKey.gameObject.SetActive(true);
			break;
		case KEY_YELLOW:
			_yellowKey.gameObject.SetActive(true);
			break;
		default:
			//Debug.Log("keyname " + keyname);
			break;
		}
	}

	//public void EndOfLevel(){
		//_goToMenuButton.gameObject.SetActive(true);
		//_goToMenuButton.enabled = true;
		//_nextButton.gameObject.SetActive(true);
		//_nextButton.enabled = true;
	//}

	public void GoToMenu(){
		_gameController.NavToMenu();
	}

	public void GoToNext(){
		_gameController.NavToNext();
	}

    private void setupButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        if (buttons.Length > 0) { 
            _goToMenuButton = buttons[1]; // reference go to menu button
            _goToMenuButton.gameObject.SetActive(false);
            _goToMenuButton.enabled = false;

            _nextButton = buttons[0]; // reference to next button
            _nextButton.gameObject.SetActive(false);
            _nextButton.enabled = false;
        }
    }

    private bool setupImages()
    {
        bool imagesExist = false;
        _currentImages = FindObjectsOfType<Image>();

        foreach (Image img in _currentImages)
        {
            switch (img.name)
            {
                case IMAGE_KEY_RED:
                    imagesExist = true;
                    _redKey = img;
                    _redKey.gameObject.SetActive(false);
                    break;
                case IMAGE_KEY_GREEN:
                    imagesExist = true;
                    _greenKey = img;
                    _greenKey.gameObject.SetActive(false);
                    break;
                case IMAGE_KEY_BLUE:
                    imagesExist = true;
                    _blueKey = img;
                    _blueKey.gameObject.SetActive(false);
                    break;
                case IMAGE_KEY_YELLOW:
                    imagesExist = true;
                    _yellowKey = img;
                    _yellowKey.gameObject.SetActive(false);
                    break;
                case IMAGE_PLAYER_COUNT_1:
                    _playerLives1 = img;
                    break;
                case IMAGE_PLAYER_COUNT_2:
                    _playerLives2 = img;
                    break;
                case IMAGE_PLAYER_COUNT_3:
                    _playerLives3 = img;
                    break;
            }
        }

        return imagesExist;
    }

    private void Visiblity(Image img, float scale, bool show)
    {
        if (show)
        {
            img.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            img.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        }
        
    }

    private void setupLivesDisplay()
    {
        bool livesCanBeSetup = false;
        if (_playerLives1 == null || _playerLives2 == null || _playerLives2 == null)
        {
            livesCanBeSetup = setupImages();
        }
        else
        {
            livesCanBeSetup = true;
        }
        
        if (livesCanBeSetup == false) return;
 
        switch (GameModel.GetInstance().playerLives)
        {
            case 1:
                Visiblity(_playerLives1, 0.2f, true);
                Visiblity(_playerLives2, 0.2f, false);
                Visiblity(_playerLives3, 0.2f, false);
                break;
            case 2:
                Visiblity(_playerLives1, 0.2f, true);
                Visiblity(_playerLives2, 0.2f, true);
                Visiblity(_playerLives3, 0.2f, false);
                break;
            case 3:
                Visiblity(_playerLives1, 0.2f, true);
                Visiblity(_playerLives2, 0.2f, true);
                Visiblity(_playerLives3, 0.2f, true);
                break;
        }
    }

    public void PopUpConfigWindowViewToggle()
    {
        _isPopUpConfigWindowOpen = !_isPopUpConfigWindowOpen;

        if (_isPopUpConfigWindowOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        configPanelWindow.SetActive(_isPopUpConfigWindowOpen);
    }

    public void ChangeBGAudioVolume(float value)
    {
        if(_audioMgr != null) _audioMgr.BgAudioVolume = value;
    }

    public void ChangeSFXVolume(float value)
    {
        if (_audioMgr != null) _audioMgr.SfxAudioVolume = value;
    }

   
}
