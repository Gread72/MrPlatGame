using UnityEngine;
using System.Collections;

public class LockKey : MonoBehaviour {

	public string keyName = "";
    public GameObject[] wallBlocks;

    [SerializeField]
	private bool isKeyFound = false;
    
    [SerializeField]
    private AudioManager _audioMgr;

    void Awake()
    {
        _audioMgr = AudioManager.GetInstance();
    }

	void OnCollisionEnter2D(Collision2D other) //Hero jumps on enemy
	{
		if (other != null && other.gameObject.tag == GameHash.HERO_TAG)
		{
			Player playScript = (Player) other.gameObject.GetComponent<Player>();
			for(int count = 0; count < playScript.keys.Count; count++){
				string keyFromArray = playScript.keys[count].ToString();
				if( System.Text.RegularExpressions.Regex.IsMatch(keyFromArray, keyName.ToString()) ){
					isKeyFound = true;
					gameObject.AddComponent<Rigidbody2D>();
					gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 500));
					gameObject.GetComponent<Collider2D>().enabled = false;
					break;
				}
			}

			if(isKeyFound == true){
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().volume = _audioMgr.SfxAudioVolume;
                    GetComponent<AudioSource>().Play();
                }

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

}
