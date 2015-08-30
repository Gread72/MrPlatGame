using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {
    [SerializeField] private GameController gm;
	
    public GameObject gameManager;
	public GameObject player;

	void Update(){
		if(player){
            gameObject.transform.position = new Vector3(player.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z); 
		}
	}

	void OnCollisionEnter2D(Collision2D other) //Hero jumps on enemy
	{
		if (other.gameObject.tag == GameHash.HERO_TAG)
		{
			gm = gameManager.GetComponent<GameController>();
            gm.PlayerLoses(other.gameObject.transform.position);
            Destroy(other.gameObject); //Remove collider to avoid audio replaying
		}else{
			Destroy(other.gameObject);
		}
	}
}
