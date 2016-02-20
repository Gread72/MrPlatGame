using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * GameModel Class - Singleton Based Model containing info on game state - level/round, score and lives 
 *
 * TODO: 
 * - Serialize game state for replayablity (high score, level, stage) - restart will player lives
 * - Restart on the last level 
 * 
 * 
 */

public class GameModel : MonoBehaviour {

    public delegate void PlayerStatusUpdate(); 

	private static GameModel _instance;
    private const int NUM_PLAYER_LIFE = 3;
    private const int MAX_NUM_LEVELS = 5;
    private const int MAX_NUM_STAGES = 3;

    [SerializeField] private int[, ,] _score = new int[MAX_NUM_STAGES, MAX_NUM_LEVELS, 1]; // multi dimensional array - stage, level, score

    [SerializeField]
    private int[, ,] _highScore = new int[MAX_NUM_STAGES, MAX_NUM_LEVELS, 1]; // multi dimensional array - stage, level, score
    [SerializeField]
    private int _lastLevel = 0;
    [SerializeField]
    private GameModel.stageName _lastStage = GameModel.stageName.STAGE_1;
    [SerializeField]
    private Dictionary<int, string> stage1Scenes = new Dictionary<int, string>();
    [SerializeField]
    private Dictionary<int, string> stage2Scenes = new Dictionary<int, string>();
    [SerializeField]
    private Dictionary<int, string> stage3Scenes = new Dictionary<int, string>();

    [SerializeField]
    private int _level;
    [SerializeField]
    private GameModel.stageName _stage;

    [SerializeField]
    private ArrayList _enemyList = new ArrayList();
    [SerializeField]
    private int _playerLives = NUM_PLAYER_LIFE;
    [SerializeField]
    private int _totalScore = 0;
    
    public Dictionary<stageName, bool> stagesPlayed = new Dictionary<stageName, bool>();

	public enum stageName{
		STAGE_1 = 0,
		STAGE_2 = 1,
		STAGE_3 = 2
	}

    public event PlayerStatusUpdate PlayerStatusUpdateEvent;

	// Use this for initialization
	void Awake () {
		if(_instance == null){
            //Debug.Log("GameModel GetInstance - new instance - level : " + _level);
			_instance = this;
            SetupStageLevelDictionary();

			DontDestroyOnLoad(this);
		}else{
			if(this != _instance) Destroy(_instance);
            //Debug.Log("GameModel GetInstance - instance exists - Destroy");
		}
	}

	static public GameModel GetInstance(){
		if(_instance == null){
			_instance = GameObject.FindObjectOfType<GameModel>();
            if(_instance == null){
                Debug.Log("GameModel GetInstance - Something went wrong. instance is null");
            }
			DontDestroyOnLoad(_instance);
		}
		return _instance;
	}

    private void SetupStageLevelDictionary()
    {
        stage1Scenes.Add(0, "level_01_stage_01");
        stage1Scenes.Add(1, "level_02_stage_01");
        stage1Scenes.Add(2, "level_03_stage_01");
        stage1Scenes.Add(3, "level_04_stage_01");
        stage1Scenes.Add(4, "level_05_stage_01");

        stage2Scenes.Add(0, "level_01_stage_02");
        stage2Scenes.Add(1, "level_02_stage_02");
        stage2Scenes.Add(2, "level_03_stage_02");
        stage2Scenes.Add(3, "level_04_stage_02");
        stage2Scenes.Add(4, "level_05_stage_02");

        stage3Scenes.Add(0, "level_01_stage_03");
        stage3Scenes.Add(1, "level_02_stage_03");
        stage3Scenes.Add(2, "level_03_stage_03");
        stage3Scenes.Add(3, "level_04_stage_03");
        stage3Scenes.Add(4, "level_05_stage_03");

        stagesPlayed.Add(stageName.STAGE_1, false);
        stagesPlayed.Add(stageName.STAGE_2, false);
        stagesPlayed.Add(stageName.STAGE_3, false);
    }

    public string GetCurrentSceneLevel()
    {
        string sceneName = "";

        switch (_lastStage)
        {
            case GameModel.stageName.STAGE_1:
                stage1Scenes.TryGetValue(_lastLevel, out sceneName);
                break;

            case GameModel.stageName.STAGE_2:
                stage2Scenes.TryGetValue(_lastLevel, out sceneName);
                break;

            case GameModel.stageName.STAGE_3:
                stage3Scenes.TryGetValue(_lastLevel, out sceneName);
                break;
        }

        return sceneName;
    }

	public int getLevelScore(){
        int currentScore = 0;

        currentScore = this._score[(int)this._stage,this._level-1,0];

        return currentScore;
	}

    public void PostGameOver()
    {
        _lastLevel = this._level - 1;
        _lastStage = this._stage;

        switch (_lastStage)
        {
            case stageName.STAGE_1:
                stagesPlayed[stageName.STAGE_1] = true;
                break;

            case stageName.STAGE_2:
                stagesPlayed[stageName.STAGE_2] = true;
                break;

            case stageName.STAGE_3:
                stagesPlayed[stageName.STAGE_3] = true;
                break;
        }

        SetTotalScore();
    }

    public int totalScore
    {
        get { return _totalScore; } 
    }

    private int SetTotalScore()
    {
        _totalScore = 0;

        for (int stageNum = 0; stageNum < MAX_NUM_STAGES; stageNum++)
        {
            for (int levelNum = 0; levelNum < MAX_NUM_LEVELS; levelNum++)
            {
                _totalScore = _totalScore + this._score[stageNum, levelNum, 0];
            }
        }

        return _totalScore;
    }

    public void setLevelScore(int value){
        this._score[(int)this._stage, this._level-1, 0] += value;

        //debugPointsScore();
    }

    public void resetLevelScore()
    {
        this._score[(int)this._stage, this._level-1, 0] = 0;
    }

    public void addBonus()
    {
        if (this._playerLives < NUM_PLAYER_LIFE)
        {
            this._playerLives = this._playerLives + 1;
        }

        CallUpdatePlayerStatus();
    }

    public void CallUpdatePlayerStatus()
    {
        if (PlayerStatusUpdateEvent != null) PlayerStatusUpdateEvent();
    }

	public int level{
		set{
            Debug.Log("level : " + value);
			this._level = value; 
		}
		get{
			//Debug.Log("score Get: " + this._score);
			return this._level; 
		}
	}

    public GameModel.stageName stage
    {
		set{ 
			this._stage = value; 
		}
		get{
			return this._stage; 
		}
	}

    public int playerLives
    {
        set
        {
            this._playerLives = value;
        }
        get
        {
            return this._playerLives;
        }
    }

	public void AddToEnemyList(string enemyType){
		_enemyList.Add(new EnemyVO(enemyType, this._level, (int)this._stage));
	}

	public int GetEnemyCount(string enemyType, int level, int stage){
        Debug.Log("GetEnemyCount type:" + enemyType + " level:" + level + " stage:" + stage);
		int count = 0;
		foreach(EnemyVO enemy in _enemyList){
			if(enemyType == enemy.enemyType && 
			   level == enemy.level && 
			   stage == enemy.stage){
			    count++;
			}
		}

		return count;
	}

    public void restartStoreGameScore()
    {
        _highScore = (int[,,])_score.Clone();

        _score = new int[MAX_NUM_STAGES, MAX_NUM_LEVELS, 1];

        _playerLives = NUM_PLAYER_LIFE;

        _lastLevel = 0;
        _lastStage = GameModel.stageName.STAGE_1;
    }

    private void debugPointsScore()
    {
        int totalScore = 0;

        int stageNum = (int)this._stage;
       
        for (int levelNum = 0; levelNum < MAX_NUM_LEVELS; levelNum++)
        {
            //Debug.Log("stage: " + stageNum + " level: " + levelNum + " score: " +this._score[stageNum, levelNum, 0]);
            totalScore = totalScore + this._score[stageNum, levelNum, 0];
                
        }
       
        //Debug.Log("totalscore " + totalScore);
    }
}
