using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject playerDiesAnimation;
	public Player playScript;
	public Text scoreText;
    public GameModel.stageName stageNumber = GameModel.stageName.STAGE_1;
	public int levelNumber = 0;
	public string resetSceneName;
	public string nextSceneName;
    public string gameOverSceneName = "GameOverScene";
	public UIMediator uiMediator;

	[SerializeField] private int _currentScore = 0;
    [SerializeField] private GameModel _refGameModel;
    
	// Use this for initialization
	void Awake () {
        _refGameModel = GameModel.GetInstance();
        _refGameModel.stage = stageNumber; //(int)GameModel.stageName.STAGE_1;
        _refGameModel.level = levelNumber; //0;

        playScript.GoalFoundEvent -= HandlerEndOfLevelGoal;
		playScript.GoalFoundEvent += HandlerEndOfLevelGoal;
	}
	
	// Update is called once per frame
	void Update () {
        int score = _refGameModel.getLevelScore();
		if(_currentScore != score){
			_currentScore = score;
			scoreText.text = "Score: " + _currentScore.ToString();
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public void KeyFound(string keyname){
		uiMediator.EnableKeyFound(keyname);
	}

	public void PlayerLoses(Vector2 position){
        if (_refGameModel.playerLives > 1)
        {
            Instantiate(playerDiesAnimation, position, Quaternion.identity);
            StartCoroutine(ResetLevel());
        }
        else
        {
            Instantiate(playerDiesAnimation, position, Quaternion.identity);
            StartCoroutine(GameOver());
        }

        _refGameModel.playerLives = _refGameModel.playerLives - 1;

        _refGameModel.CallUpdatePlayerStatus();
	}

	public void HandlerEndOfLevelGoal(){
        uiMediator.endOfRoundWindow.SetActive(true);
        
        _refGameModel.PostGameOver();

        int enemyCount = _refGameModel.GetEnemyCount("Enemy_01", _refGameModel.level, (int)_refGameModel.stage);
        uiMediator.StatusText.text = "Total Score: " + _refGameModel.totalScore + " \nMonsters Killed: " + enemyCount + "\nKarma Points: 9";
        Time.timeScale = 0;
		//NavToNext();
	}

	IEnumerator ResetLevel(){
		yield return new WaitForSeconds(1);
        _refGameModel.resetLevelScore();
		Application.LoadLevel(resetSceneName);
	}

	IEnumerator NextLevel(){
		yield return new WaitForSeconds(1);
		Application.LoadLevel(nextSceneName);
	}

    IEnumerator GameOver()
    {
        //Debug.Log("Game Over - level: " + _refGameModel.level + " stage:" + _refGameModel.stage);

        _refGameModel.PostGameOver();

        yield return new WaitForSeconds(2);
        Application.LoadLevel(gameOverSceneName);
    }

	public void NavToMenu(){
		Application.LoadLevel("StartMenu");
	}

	public void NavToNext(){
        Time.timeScale = 1;
		StartCoroutine(NextLevel());
	}

    public void ReplayRound()
    {
        Time.timeScale = 1;
        StartCoroutine(ResetLevel());
    }

}
