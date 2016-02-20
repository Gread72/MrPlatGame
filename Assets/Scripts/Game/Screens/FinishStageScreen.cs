using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*
 * FinishStageScreen Class - Mediator for Finish/End Screen
 *
 * TODO: 
 * - Change the button state - Restart on the last level 
 * 
 * 
 */

public class FinishStageScreen : MonoBehaviour {

    public string nextSceneName;

    [SerializeField] private GameModel _refGameModel;

	// Use this for initialization
	void Start () {
        _refGameModel = GameModel.GetInstance();

        StartCoroutine(GameCompletedStage());
	}

    IEnumerator GameCompletedStage()
    {
        if (_refGameModel != null){ 
            Debug.Log("Game Over - level: " + _refGameModel.level + " stage:" + _refGameModel.stage);
            _refGameModel.PostGameOver();
        }

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextSceneName);
        //Application.LoadLevel(nextSceneName);
    }
}
