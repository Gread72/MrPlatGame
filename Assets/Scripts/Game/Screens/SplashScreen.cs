using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * SplashScreen Class - Mediator for Splash Screen - basically loading the scenes
 *
 * Note: this means to load scene is not as useful as expected
 */


public class SplashScreen : MonoBehaviour {

    public Text progressText;
    public string levelToLoad;

    [SerializeField] private float _loadProgress = 0f;

	void Start () {
        StartCoroutine(ReadySteadyGo());
	}

    IEnumerator ReadySteadyGo()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(levelToLoad);
        //Application.LoadLevelAsync(levelToLoad);

        while (!async.isDone)
        {
            _loadProgress = async.progress;

            progressText.text = "Loading ... " + Mathf.Round(_loadProgress * 100) + "%";

            yield return null;
           
        }

    }
	
}
