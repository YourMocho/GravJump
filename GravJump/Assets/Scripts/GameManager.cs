using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject screensParent;

    public static LevelMover levelMover;
    public static PlayerController playerController;
    public static InvertSpawner invertSpawner;
    private static Text scoreText;
    public static GameObject pauseButton;
    public static LevelCreator levelCreator;
    public static Canvas canvas;
    public static CircleCollider2D playerJumpTrigger;

    private static GameObject countdownScreen;
    private static GameObject gameOverScreen;
    private static GameObject pauseScreen;
    private static Text countdownText;
    private static Text endScoreText;
    private static Text bestScoreText;

    public static int checkpointRespawns;
    private static int score;
    private static int countdown;
    private static float timer;
    public static bool gameStarted;
    public static bool paused;
    public static int startSpeed;
    public static bool gameOver;
    public static bool playerIsReseting;

    public static Checkpoint lastCheckpoint;

    public static bool gravityIsDown;

    public static Vector2 gravity;

    public static Color blockColour;
   // public static Color blockColour = new Color(43/255f, 130/ 255f,188/ 255f); //inverted

    void Awake()
    {
        print("GM awake");
        screensParent.SetActive(true);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        invertSpawner = GameObject.Find("InvertSpawner").GetComponent<InvertSpawner>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerJumpTrigger = GameObject.Find("JumpTrigger").GetComponent<CircleCollider2D>();
        levelCreator = GameObject.Find("LevelAnchor").GetComponent<LevelCreator>();

        countdownScreen = GameObject.Find("CountdownScreen");
        gameOverScreen = GameObject.Find("GameOverScreen");
        endScoreText = gameOverScreen.transform.GetChild(1).GetComponent<Text>();
        bestScoreText = gameOverScreen.transform.GetChild(2).GetComponent<Text>();
        pauseScreen = GameObject.Find("PauseScreen");
        countdownText = countdownScreen.transform.GetChild(0).GetComponent<Text>();
        pauseButton = GameObject.Find("PauseButton");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "";

        countdownScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        checkpointRespawns = 5;
        score = 0;
        paused = false;
        startSpeed = 10;
        gameOver = false;
        playerIsReseting = false;

        gravityIsDown = true;
        gravity = new Vector2(0.0f, -9.8f);
        //blockColour = new Color(212 / 255f, 125 / 255f, 67 / 255f);
        blockColour = new Color(181 / 255f, 151 / 255f, 85 / 255f);
    }

    void Start() { 
        StartCountdown();
    }
	
	void Update () {
        if (countdown > 0)
        {
            if (!paused)
            {
                countdownText.text = countdown.ToString();
            }

            if (Time.time - timer > 1.0f)
            {
                countdown--;
                timer = Time.time;
            }
        }
        if (countdown == 0)
        {
            StartGame();
            countdown = -1;
        }
        if(!gameStarted && paused)
        {
            timer += Time.deltaTime;
        }

        calculateScore();

        if (score != 0)
        {
            scoreText.text = score.ToString();
        }

        if (playerIsReseting)
        {
            playerController.GetComponent<Rigidbody2D>().velocity = new Vector2(levelMover.Xspeed, playerController.GetComponent<Rigidbody2D>().velocity.y);

            if (playerController.transform.position.x >= 0)
            {
                playerController.transform.position = new Vector2(0, playerController.transform.position.y);
                playerIsReseting = false;
            }
        }
    }

    public static void StartGame()
    {
        countdownScreen.SetActive(false);

        gameStarted = true;
        Physics2D.gravity = gravity;
    }

    public static void StartCountdown()
    {
        countdownScreen.SetActive(true);

        gameStarted = false;
        Physics2D.gravity = Vector2.zero;
        countdown = 3;
        timer = Time.time;
        paused = false;
    }

    public static void PlayerDied()
    {
 

        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }

        if (!gameOver)
        {
            ResetMapAndPlayer();
            StartCountdown();
            print("player has respawned");
        }
    }

    public static void ResetMapAndPlayer()
    {
        playerController.RespawnPlayer();
        ResetMapToSpawnPoint();
        ResetPickUps();
    }

    private static void ResetPickUps()
    {
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        foreach(GameObject pickUp in pickUps)
        {
            pickUp.GetComponent<SpriteRenderer>().enabled = true;
            pickUp.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    public static void ResetMapToSpawnPoint()
    {
        levelMover.transform.position = levelMover.levelSpawnPoint;
    }

    private void calculateScore()
    {
        score = (int)(levelMover.transform.position.x * -1);
    }

    public static void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            paused = true;
            print("game is paused");
            pauseButton.SetActive(false);
            pauseScreen.SetActive(true);
            countdownScreen.SetActive(false);
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            paused = false;
            print("game is resumed");
            pauseButton.SetActive(true);
            pauseScreen.SetActive(false);
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            if (countdown > 0)
            {
                countdownScreen.SetActive(true);
            }
        }
    }

    public static void InvertColours()
    {
        invertSpawner.Invert();
    }

    public static void FlipInvisibleBlocks()
    {
        foreach(LevelPiece levelPiece in levelCreator.currentLevelPieces)
        {
            levelPiece.FlipInvisibleBlocks();
        }
    }

    public static void NextLevel()
    {
        levelMover.levelSpawnPoint = new Vector3(0, levelMover.levelSpawnPoint.y, levelMover.levelSpawnPoint.z); ;
        playerController.spawnPoint = Vector3.zero;
        PlayerDied();
    }

    public static void SetCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
        levelMover.levelSpawnPoint = new Vector3(checkpoint.startingPosition.x,levelMover.levelSpawnPoint.y, levelMover.levelSpawnPoint.z); //levelMover.transform.position.x;
        playerController.spawnPoint = new Vector3(0, checkpoint.transform.position.y, 0);
        levelCreator.RecordDeathsAtCheckpoint();
        levelCreator.checkpointPiece = checkpoint.transform.parent.GetComponent<LevelPiece>();
        levelCreator.RemovePiecesUntilLastCheckpoint();
    }

    public static void GameOver()
    {
        gameOver = true;
        pauseButton.SetActive(false);
        gameOverScreen.SetActive(true);
        endScoreText.text = "Score\n" + scoreText.text;
        bestScoreText.text = "Best\n" + PlayerPrefs.GetInt("score").ToString();
        print("game over");
    }
}
