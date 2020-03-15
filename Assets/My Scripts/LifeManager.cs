using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LifeManager : MonoBehaviour {

    public int startingLives = 1;
    public int lifeCounter;
    
    private Text theText;

	// Use this for initialization
	void Start ()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;
	}
	
	// Update is called once per frame
	void Update ()
    {
        theText.text = "x " + lifeCounter;
	}

    public void GiveLife()
    {
        lifeCounter++;
    }

    public void TakeLife()
    {
        lifeCounter--;

        if(lifeCounter <= 0)
        {
            lifeCounter = 0;
        }
    }

}
