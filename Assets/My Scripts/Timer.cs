using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using FullSerializer;
using DG.Tweening;
using enableGame;

public class Timer : MonoBehaviour
{
    float timeLeft;
    public Text countdownText;
    int countDownStart = 5;

   
    // Update is called once per frame
    void Update()
    {

         if (countDownStart > 0)
         {
            timeLeft += Time.deltaTime;
            countdownText.text = (countDownStart - Mathf.Floor(timeLeft)).ToString();
         }

        if (countDownStart - Mathf.Floor(timeLeft) <= 0)
        {
            SceneManager.LoadScene("Level1");
            Tracker.Instance.Interrupt((int)egEvent.Type.CustomEvent, "GameEnd");
            Tracker.Instance.StopTracking();
            NetworkClientConnect.Instance.Disconnect(); // this will disconnect form the avatar server! remember to disconnect each time you change the time scale or you change scene
            Tracker.Instance.StopTracking();
        }
    }
}
