using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {


    public static LevelMover levelMover;
    public static PlayerController playerController;
    public static InvertSpawner invertSpawner;
    private static Text scoreText;
    private static GameObject pauseButton;
    public static LevelCreator levelCreator;
    public static Canvas canvas;
    public static CircleCollider2D playerJumpTrigger;

    private static GameObject countdownScreen;
    private static GameObject gameOverScreen;
    private static GameObject pauseScreen;
    private static Text countdownText;


    private static int score = 0;
    private static int countdown;
    private static float timer;
    public static bool gameStarted;
    public static bool paused = false;
    public static int startSpeed = 10;
    public static int maxSpeed = 25;
    public static int minSpeed = 3;

    public static Checkpoint lastCheckpoint = null;

    public static bool gravityIsDown = true;

    public static Vector2 gravity = new Vector2(0.0f, -9.8f);

    public static Color blockColour = new Color(212 / 255f, 125 / 255f, 67 / 255f);
   // public static Color blockColour = new Color(43/255f, 130/ 255f,188/ 255f); //inverted

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        invertSpawner = GameObject.Find("InvertSpawner").GetComponent<InvertSpawner>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerJumpTrigger = GameObject.Find("JumpTrigger").GetComponent<CircleCollider2D>();
        levelCreator = GameObject.Find("LevelAnchor").GetComponent<LevelCreator>();

        countdownScreen = GameObject.Find("CountdownScreen");
        gameOverScreen = GameObject.Find("GameOverScreen");
        pauseScreen = GameObject.Find("PauseScreen");
        countdownText = countdownScreen.transform.GetChild(0).GetComponent<Text>();
        pauseButton = GameObject.Find("PauseButton");
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "";

        countdownScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerForPickUp();
        }
    }

    public static void StartGame()
    {
        countdownScreen.SetActive(false);

       // countdownText.text = "";
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
        print("player has died");

        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }

        if (lastCheckpoint != null)
        {
            lastCheckpoint.UpdateRespawnNumber();
        }

        playerController.RespawnPlayer();
        StartCountdown();
        ResetMapToSpawnPoint();
        ResetPickUps();

    }

    private static void ResetPickUps()
    {
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("PickUp");
        foreach(GameObject pickUp in pickUps)
        {
            pickUp.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public static void ResetPlayerForPickUp()
    {
        float x = playerController.spawnPoint.x - playerController.transform.position.x;
        playerController.transform.position = new Vector3(playerController.spawnPoint.x, playerController.transform.position.y, 0);
        levelMover.transform.Translate(new Vector3(x, 0, 0));
    }

    public static void ResetMapToSpawnPoint()
    {
        levelMover.transform.position = levelMover.levelSpawnPoint;
    }

    private void calculateScore()
    {
        score = (int)(levelMover.transform.position.x * -1);
    }

    public static void TogglePause()
    {
        paused = !paused;

        if (paused)
        {
            print("game is paused");
            pauseButton.SetActive(false);
            pauseScreen.SetActive(true);
            countdownScreen.SetActive(false);
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
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
        //SceneManager.LoadScene("Menu");
        gameOverScreen.SetActive(true);
    }

    public static void ShowPauseButton(bool state)
    {
        pauseButton.SetActive(state);
    }
}
