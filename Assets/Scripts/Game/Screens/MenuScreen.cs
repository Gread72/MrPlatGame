using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * TODO: 
 * - Change the button state - Restart on the last level 
 * 
 * 
 */

public class MenuScreen : MonoBehaviour {

	public Text scoreCore;
    public Button StartContinueButton;
    public Button Stage1Button;
    public Button Stage2Button;
    public Button Stage3Button;
    public string startGameScene = "level_01_stage_01";

   [HideInInspector]  [SerializeField] private GameModel _refGameModel;

    void Awake()
    {
        _refGameModel = GameModel.GetInstance();
    }

	// Use this for initialization
	void Start () {
        
        if (scoreCore == null)
        {
            Debug.Log("MenuScreen - Something went wrong.");
        }

        int score = 0;
        if (_refGameModel)
        {
            score = _refGameModel.totalScore;
        }
       
        if (score > 0)
        {
            scoreCore.gameObject.SetActive(true);
            scoreCore.text = "Total Score: " + score.ToString();
        }
        else
        {
            scoreCore.gameObject.SetActive(false);
        }

        SetButtons();
	}

    private void SetButtons()
    {
        
        if (_refGameModel.stagesPlayed[GameModel.stageName.STAGE_1])
        {
           if(Stage1Button) Stage1Button.gameObject.SetActive(true);
           if(StartContinueButton) StartContinueButton.GetComponentInChildren<Text>().text = "Continue"; /// if play has reached the first level continue game
        }
        else
        {
           if (Stage1Button) Stage1Button.gameObject.SetActive(false);
        }

        if (_refGameModel.stagesPlayed[GameModel.stageName.STAGE_2])
        {
            if (Stage2Button) Stage2Button.gameObject.SetActive(true);
        }
        else
        {
            if (Stage2Button) Stage2Button.gameObject.SetActive(false);
        }

        if (_refGameModel.stagesPlayed[GameModel.stageName.STAGE_3])
        {
            if (Stage3Button)  Stage3Button.gameObject.SetActive(true);
        }
        else
        {
            if (Stage3Button)  Stage3Button.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame(){

        startGameScene = _refGameModel.GetCurrentSceneLevel();
      
       _refGameModel.restartStoreGameScore();

        ReloadScene();
	}

    public void ReloadScene()
    {
        Application.LoadLevel(startGameScene);
    }

    public void StartGame_Stage1()
    {
     
        startGameScene = "level_01_stage_01";
        _refGameModel.level = 0;
        _refGameModel.stage = GameModel.stageName.STAGE_1;
        _refGameModel.playerLives = 3;

        Application.LoadLevel(startGameScene);
    }

    public void StartGame_Stage2()
    {
        startGameScene = "level_01_stage_02";
        _refGameModel.level = 0;
        _refGameModel.stage = GameModel.stageName.STAGE_2;
        _refGameModel.playerLives = 3;

        Application.LoadLevel(startGameScene);
    }

    public void StartGame_Stage3()
    {
        startGameScene = "level_01_stage_03";
        _refGameModel.level = 0;
        _refGameModel.stage = GameModel.stageName.STAGE_3;
        _refGameModel.playerLives = 3;

        Application.LoadLevel(startGameScene);
    }

    public void GoToGameMenu()
    {
        Application.LoadLevel("StartMenu");
    }
}
