using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static Text countdownText;
    private static GameObject textBackground;
    public static LevelMover levelMover;
    public static PlayerController playerController;
    public static GameObject invertColoursPlane;
    public static InvertSpawner invertSpawner;
    private static Text scoreText;
    private static GameObject backButton;
    private static GameObject pauseButton;
    public static LevelCreator levelCreator;
    public static Canvas canvas;
    public static CircleCollider2D playerJumpTrigger;

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

    private static Vector2 gravity = new Vector2(0.0f, -9.8f);

    public static Color blockColour = new Color(212 / 255f, 125 / 255f, 67 / 255f);
   // public static Color blockColour = new Color(43/255f, 130/ 255f,188/ 255f); //inverted

    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        invertColoursPlane = GameObject.Find("InvertColoursPlane");
        invertColoursPlane.SetActive(false);
        invertSpawner = GameObject.Find("InvertSpawner").GetComponent<InvertSpawner>();
        backButton = GameObject.Find("BackButton");
        backButton.SetActive(false);
        pauseButton = GameObject.Find("PauseButton");
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        textBackground = GameObject.Find("TextBackground");
        playerJumpTrigger = GameObject.Find("JumpTrigger").GetComponent<CircleCollider2D>();
        levelCreator = GameObject.Find("LevelAnchor").GetComponent<LevelCreator>();
        scoreText.text = "";
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
        textBackground.SetActive(false);
        countdownText.text = "";
        gameStarted = true;
        Physics2D.gravity = gravity;
    }

    public static void StartCountdown()
    {
        gameStarted = false;
        Physics2D.gravity = Vector2.zero;
        textBackground.SetActive(true);
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
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            countdownText.text = "Paused";
            backButton.SetActive(true);
            textBackground.SetActive(true);
        }
        else
        {
            print("game is resumed");
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            countdownText.text = "";
            backButton.SetActive(false);
            textBackground.SetActive(false);
        }
    }

    public static void InvertColours()
    {
        //invertColoursPlane.SetActive(!invertColoursPlane.activeSelf);

        //invertColoursCircle.Reverse();
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
        SceneManager.LoadScene("Menu");
    }

    public static void ShowPauseButton(bool state)
    {
        pauseButton.SetActive(state);
    }
}
