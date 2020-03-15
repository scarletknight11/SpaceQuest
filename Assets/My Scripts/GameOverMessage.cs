using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class GameOverMessage : MonoBehaviour {

    public int score;

    void Start()
    {
        score = 0;
 
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //checks other collider's tag
        if(other.gameObject.tag == "Pickup")
        {
            score++;
            Destroy(other.gameObject);
        }
    }
}