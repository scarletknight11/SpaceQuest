using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownController2 : MonoBehaviour {

    float currentTime = 0f;
    float startimgTime = 10f;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
     void Start()
     {
        currentTime = startimgTime;
     }

     void Update()
     {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0;
            SceneManager.LoadScene("Level3");
        }
    }
}
