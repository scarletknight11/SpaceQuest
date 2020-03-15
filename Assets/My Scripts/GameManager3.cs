using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour {

    public static GameManager3 gm3;

    public GameObject player;

    public enum gameStates { Playing, Death, GameOver, BeatLevel };
    public gameStates gameState = gameStates.Playing;

    public int score = 0;
    public bool canBeatLevel = false;
    public int beatLevelScore = 0;
    private Health playerHealth;

    public GameObject mainCanvas;
    public Text mainScoreDisplay;
    public GameObject gameOverCanvas;
    public Text gameOverScoreDisplay;

    //public GameObject beatLevelCanvas;


    // Use this for initialization
    void Start()
    {
        if (gm3 == null)
            gm3 = gameObject.GetComponent<GameManager3>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        playerHealth = player.GetComponent<Health>();

        // setup score display
        Collect3(0);

        // make other UI inactive
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            //While during gameplay
            case gameStates.Playing:
                if (playerHealth.isAlive == false)
                {
                    //update gameState
                    gameState = gameStates.Death;

                    //set the end game score
                    gameOverScoreDisplay.text = mainScoreDisplay.text;

                    //switch which GUI is showing		
                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(false);
                }
                else if (canBeatLevel && score >= beatLevelScore)
                {
                    // update gameState
                    gameState = gameStates.BeatLevel;

                    //hide the player so game doesn't continue playing
                    player.SetActive(false);

                    //switch which GUI is showing			
                    mainCanvas.SetActive(false);
                    //beatLevelCanvas.SetActive(true);
                }
                break;
        }
    }

    public void Collect3(int amount)
    {
        score += amount;
        if (canBeatLevel) {
            mainScoreDisplay.text = "Score: " + score.ToString(); //+ " of " + beatLevelScore.ToString();
        } else {
            mainScoreDisplay.text = score.ToString();
        }
    }
}
