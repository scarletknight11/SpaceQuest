using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    [Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
    public GameObject player;

    public enum gameStates {Playing, Death, GameOver, BeatLevel};
    public gameStates gameState = gameStates.Playing;

    public int score = 0;
    public bool canBeatLevel = false;
    public int beatLevelScore;

    public GameObject mainCanvas;
    public Text mainScoreDisplay;
    public GameObject gameOverCanvas;
    public Text gameOverScoreDisplay;

    [Tooltip("Only need to set if canBeatLevel is set to true.")]
    public GameObject beatLevelCanvas;

    public AudioSource backgroundMusic;
    public AudioClip gameOverSFX;

    [Tooltip("Only need to set if canBeatLevel is set to true.")]
    public AudioClip beatLevelSFX;

    private Health playerHealth;
 
    void Start() {
 
        if (gm == null)
            gm = gameObject.GetComponent<GameManager>();

        if (player == null) {
            player = GameObject.FindWithTag("Player");
        }

        playerHealth = player.GetComponent<Health>();

        // setup score display
        Collect(0);

        // make other UI inactive
        gameOverCanvas.SetActive(false);
        if (canBeatLevel)
            beatLevelCanvas.SetActive(false);
    }

    void Update()
    {
        switch (gameState)
        {
            //While during gameplay
            case gameStates.Playing:
                if (playerHealth.isAlive == false)
                {
                    // update gameState
                    gameState = gameStates.Death;

                    // set the end game score
                    gameOverScoreDisplay.text = mainScoreDisplay.text;

                    // switch which GUI is showing		
                    mainCanvas.SetActive(false);
                    gameOverCanvas.SetActive(true);
                }
                else if (canBeatLevel && score >= beatLevelScore)
                {
                    // update gameState
                    gameState = gameStates.BeatLevel;

                    // hide the player so game doesn't continue playing
                    player.SetActive(false);

                    // switch which GUI is showing			
                    mainCanvas.SetActive(false);
                    beatLevelCanvas.SetActive(true);
                }
                break;
            //When Gameplay Ends
            case gameStates.Death:
                backgroundMusic.volume -= 0.01f;
                if (backgroundMusic.volume <= 0.0f)
                {
                    AudioSource.PlayClipAtPoint(gameOverSFX, gameObject.transform.position);

                    gameState = gameStates.GameOver;
                }
                break;
            //When you sucessfully beat level
            case gameStates.BeatLevel:
                backgroundMusic.volume -= 0.01f;
                if (backgroundMusic.volume <= 0.0f)
                {
                    AudioSource.PlayClipAtPoint(beatLevelSFX, gameObject.transform.position);

                    gameState = gameStates.GameOver;
                }
                break;
            //Generic. Gameover could mean you either won or lost
            case gameStates.GameOver:
                // nothing
                break;
        }
    }


        public void Collect(int amount) {
        score += amount;
        if (canBeatLevel) {
            mainScoreDisplay.text = score.ToString() + " of " + beatLevelScore.ToString();
        } else {
            mainScoreDisplay.text = score.ToString();
        }
    }

    public void ScoreReset() {
        score -= 2;
        mainScoreDisplay.text = score.ToString() + " of " + beatLevelScore.ToString();
        if (score <= 0)
        {
            score = 0;
            mainScoreDisplay.text = score.ToString() + " of " + beatLevelScore.ToString();
        }

    }
}
