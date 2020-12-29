using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using enableGame;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class Mainmenu : MonoBehaviour {

    public static bool isPaused;

	public void MainMenu()
	{
		print("MainMenu");
		UnPauseGame();  //must start up unity time again so DOTweens work
		egEndSession();
		SceneManager.LoadScene("eag_MainMenu3");
	}

	public void UnPauseGame()
	{
		//print("Unpause");
		isPaused = false;
		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02f;
		AudioListener.volume = 1.0f;
	}
	void egEndSession()
	{
		Tracker.Instance.Interrupt((int)egEvent.Type.CustomEvent, "GameEnd");
		Tracker.Instance.StopTracking();
		NetworkClientConnect.Instance.Disconnect(); // this will disconnect form the avatar server! remember to disconnect each time you change the time scale or you change scene
	}


}
