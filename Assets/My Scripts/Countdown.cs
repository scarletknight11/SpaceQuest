using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public int timeLeft = 5; //Seconds Overall
    public Text countdown; //UI Text Object


    void Start()
    {
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        countdown.text = (" " + timeLeft); //Showing the Score on the Canvas
    }
    //Simple Coroutine
    IEnumerator LoseTime(Text tm)
    {
        while (true)
        {

            float start = Time.realtimeSinceStartup;
            int time = timeLeft;
            while (Time.realtimeSinceStartup < start + timeLeft)
            {
                tm.text = Mathf.Round(((start + timeLeft) - Time.realtimeSinceStartup)).ToString();
                timeLeft--;
                SceneManager.LoadScene("Level2");
                Tracker.Instance.Interrupt((int)egEvent.Type.CustomEvent, "GameEnd");
                Tracker.Instance.StopTracking();
                NetworkClientConnect.Instance.Disconnect(); // this will disconnect form the avatar server! remember to disconnect each time you change the time scale or you change scene
                Tracker.Instance.StopTracking();
                yield return null;
            }
            // yield return new WaitForSeconds(1);

        }
    }
}