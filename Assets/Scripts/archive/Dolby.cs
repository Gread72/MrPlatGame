using UnityEngine;
using System.Collections;

using System.Runtime.InteropServices; //Allows us to use DLLImport

public class Dolby : MonoBehaviour
{
	private GameObject debugText;
	public Font arial; 
	
	/* Import plugin functions */
	
	[DllImport("DSPlugin")]
	public static extern  bool isAvailable();
	[DllImport("DSPlugin")]
	public static extern  int initialize();
	[DllImport("DSPlugin")]
	public static extern  int setProfile(int profileid);
	[DllImport("DSPlugin")]
	public static extern  int suspendSession();
	[DllImport("DSPlugin")]
	public static extern  int restartSession();
	[DllImport("DSPlugin")]
	public static extern void release();
	
	// Use this for initialization
	void Start()
	{
		/* Textfield created for feedback */       
		debugText = new GameObject();
		debugText.AddComponent<GUIText>();
		debugText.GetComponent<GUIText>().font = arial;
		debugText.GetComponent<GUIText>().fontSize = 14;
		debugText.GetComponent<GUIText>().color = new Color(255, 0, 0);
		debugText.transform.position = new Vector3(0, 1, 0);
		
		/* Initialize Dolby if Available */
		
		if (isAvailable())
		{
			Invoke("Init", 0.1f); // Wait 100ms to make sure Dolby service is enabled
		}
		else
		{
			debugText.GetComponent<GUIText>().text = "Dolby Sound Not Available";
		}
	}
	
	void Init()
	{
		debugText.GetComponent<GUIText>().text = "Dolby Sound Available";
		setProfile(2); /* Set Profile to "Game" */
		initialize();
	}
	
	void OnApplicationPause()
	{
		suspendSession();// Dolby sound stops if app switched or paused
	}
	
	void OnApplicationFocus()
	{
		restartSession(); // Restart Dolby sound if app is active
	}
	
	void OnApplicationQuit()
	{
		release(); //Stops Dolby Sound completely
	}
}