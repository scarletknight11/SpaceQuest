﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using FullSerializer;
using DG.Tweening;


public class CountdownScript1 : MonoBehaviour
{

    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer = 5;
    private bool canCount = true;
    private bool doOnce = false;
 

    // Use this for initialization
    void Awake()
    {
        StartCoroutine(Test());
        timer = mainTimer;
        canCount = true;
        doOnce = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(3);

        if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("f0");
            //StartCoroutine(Test());
            //uiText.text = "5";
            //uiText.text = "4";
            //uiText.text = "3";
            //uiText.text = "2";
            //uiText.text = "1";
            //uiText.text = "0";
            SceneManager.LoadScene("Level2");
            Tracker.Instance.Interrupt((int)egEvent.Type.CustomEvent, "GameEnd");
            Tracker.Instance.StopTracking();
            NetworkClientConnect.Instance.Disconnect(); // this will disconnect form the avatar server! remember to disconnect each time you change the time scale or you change scene
            NetworkClientConnect.Instance.Connect(); //added connection here
            //Tracker.Instance.StopTracking();
        }

        while (timer >= 0.0f && canCount)
        {

            timer -= Time.realtimeSinceStartup;
            uiText.text = timer.ToString("F");

            if (Time.realtimeSinceStartup <= 0.0f && !doOnce)
            {
                canCount = false;
                doOnce = true;
                uiText.text = "0.00";
                timer = 0.0f;
            }
        }
    }
}