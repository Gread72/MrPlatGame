using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SplashScreen : MonoBehaviour {

    public Text progressText;
    public string levelToLoad;

    [SerializeField] private float _loadProgress = 0f;

	void Start () {
        StartCoroutine(ReadySteadyGo());
	}

    IEnumerator ReadySteadyGo()
    {
        AsyncOperation async = Application.LoadLevelAsync(levelToLoad);

        while (!async.isDone)
        {
            _loadProgress = async.progress;

            progressText.text = "Loading ... " + Mathf.Round(_loadProgress * 100) + "%";

            yield return null;
           
        }

        //Debug.Log(async.isDone);

    }
	
}
