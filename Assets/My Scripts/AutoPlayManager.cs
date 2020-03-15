using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using enableGame;
using eaglib;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class AutoPlayManager : MonoBehaviour {

 //   public RBPauseManager pm;
 //   public TextMesh finishPanelNumber;

 //   public egBool autoplay = new bool();

 //   int cd = 5;

	////Use this for initialization
	//void Awake () {
 //       VariableHandler.Instance.Register(RBParameterStrings.AUTOPLAY, autoplay);
 //   }

 //   // Update is called once per frame
 //   void Update () {
		
	//}

 //   public void finishPanel(bool lastLevel) {
 //       if (autoplay)
 //       {
 //          // finishPanelPlayBtn.SetActive(false);
 //           cd = 5;
 //           StartCoroutine(cooldown(finishPanelNumber));
 //           if (!lastLevel)
 //           {
 //               StartCoroutine(startAutoplay(0));
 //           }
 //           else
 //           {
 //               StartCoroutine(startGameOver());
 //           }
 //       }
 //       else
 //       {
 //           finishPanelNumber.gameObject.SetActive(false);
 //       }
 //   }


 //   bool countdown = false;

 //   IEnumerator startGameOver()
 //   {
 //       while (countdown)
 //       {
 //           yield return null;
 //       }
 //   }

 //   IEnumerator startAutoplay(int i)
 //   {
 //       while (countdown)
 //       {
 //           yield return null;
 //       }
 //       switch (i)
 //       {
 //           case 0:
 //               pm.autoplay();
 //               break;
 //           case 1:
 //              // pm.autostart();
 //               break;
 //       }
 //   }

 //   public IEnumerator cooldown(TextMesh tm) {

 //       countdown = true;

 //       float start = Time.realtimeSinceStartup;
 //       int time = cd;
 //       while (Time.realtimeSinceStartup < start + cd)
 //       {
 //           tm.text = Mathf.Round(((start + cd) - Time.realtimeSinceStartup)).ToString();
 //           yield return null;
 //       }
 //       SceneManager.LoadScene("Level2");
 //       Tracker.Instance.Interrupt((int)egEvent.Type.CustomEvent, "GameEnd");
 //       Tracker.Instance.StopTracking();
 //       NetworkClientConnect.Instance.Disconnect(); // this will disconnect form the avatar server! remember to disconnect each time you change the time scale or you change scene
 //       Tracker.Instance.StopTracking();
 //       countdown = false;
 //   }
}
