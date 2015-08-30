using UnityEngine;
using System.Collections;

public class DestroyWalls : MonoBehaviour {

	public GameObject[] wallBlocks;

    [SerializeField] private bool isWallDestroyed = false;
    [SerializeField] private AudioManager _audioMgr;

    void Awake()
    {
       _audioMgr = AudioManager.GetInstance();
    }
	
	void OnTriggerEnter2D( Collider2D other ){
		if (other != null && other.gameObject.tag == GameHash.HERO_TAG)
		{
            if (GetComponent<AudioSource>() && isWallDestroyed == false)
            {
                GetComponent<AudioSource>().volume = _audioMgr.SfxAudioVolume;
                GetComponent<AudioSource>().Play();
            }

            gameObject.GetComponent<Collider2D>().isTrigger = true;
            isWallDestroyed = true;
			int multiplyPos = 1;
			foreach(GameObject item in wallBlocks)
			{
				if(item != null)
				{
					if(item.GetComponent<Rigidbody2D>() == null)
					{
						item.AddComponent<Rigidbody2D>();
						item.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 100 * multiplyPos));
					}
					item.GetComponent<Collider2D>().enabled = false;
					multiplyPos = multiplyPos + 1;
				}
			}

		}
	}
}
