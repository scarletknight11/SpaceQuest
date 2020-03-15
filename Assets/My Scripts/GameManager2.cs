using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {

    public static GameManager2 gm2;

    public GameObject player;

    public enum gameStates {Playing, Death, GameOver, BeatLevel};
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
    void Start() {
        if (gm2 == null)
            gm2 = gameObject.GetComponent<GameManager2>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        playerHealth = player.GetComponent<Health>();

        // setup score display
        Collect2(0);

        // make other UI inactive
        gameOverCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        switch (gameState)
        {
            //While during gameplay
            case gameStates.Playing:
                if (playerHealth.isAlive == false) {
                    //update gameState
                    gameState = gameStates.Death;

                    //set the end game score
                    gameOverScoreDisplay.text = mainScoreDisplay.text;

                    //switch which GUI is showing		
                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(false);
                }
                else if (canBeatLevel && score >= beatLevelScore) {
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

    public void Collect2(int amount) {
        score += amount;
        if (canBeatLevel) {
            // mainScoreDisplay.text = "You Scored " + score.ToString() + " of " + beatLevelScore.ToString();
            mainScoreDisplay.text = "Score: " + score.ToString(); //+ " of " + beatLevelScore.ToString();

        }
        else {
            mainScoreDisplay.text = score.ToString();
        }
    }
}