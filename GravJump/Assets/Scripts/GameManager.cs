using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    private static Text countdownText;
    private static GameObject textBackground;
    public static LevelMover levelMover;
    public static PlayerController playerController;
    public static GameObject invertColoursPlane;
    private static Text scoreText;
    private static GameObject backButton;
    public static Canvas canvas;

    private static int score = 0;
    private static int countdown;
    private static float timer;
    public static bool gameStarted;
    public static bool paused = false;
    public static int startSpeed = 10;
    public static int maxSpeed = 25;
    public static int minSpeed = 3;

    public static Color normalColour; 
    public static Color touchingColour = new Color(67 / 255f,154 / 255f, 212 / 255f);

    private static Vector2 gravity = new Vector2(0.0f, -9.8f);

    void Awake()
    {
        //   normalColour = GameObject.Find("GroundBlock").GetComponent<SpriteRenderer>().color;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        invertColoursPlane = GameObject.Find("InvertColoursPlane");
        invertColoursPlane.SetActive(false);
        backButton = GameObject.Find("BackButton");
        backButton.SetActive(false);
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        textBackground = GameObject.Find("TextBackground");
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
        levelMover.SetSpeed(startSpeed);
        playerController.RemoveAllVelocity();
        playerController.ResetGravityDirectionAndColours();
    }

    public static void PlayerDied()
    {
        print("player has died");

        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
        ResetMapToSpawnPoint();
        ResetPickUps();
        playerController.RespawnPlayer();
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
        float x = playerController.lastCheckpoint.x - playerController.transform.position.x;
        playerController.transform.position = new Vector3(playerController.lastCheckpoint.x, playerController.transform.position.y, 0);
        levelMover.transform.Translate(new Vector3(x, 0, 0));
    }

    public static void ResetMapToSpawnPoint()
    {
        levelMover.transform.position = new Vector3(levelMover.levelSpawnPoint, 0, 0);
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
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            countdownText.text = "Paused";
            backButton.SetActive(true);
            textBackground.SetActive(true);
        }
        else
        {
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            countdownText.text = "";
            backButton.SetActive(false);
            textBackground.SetActive(false);
        }
    }

    public static void InvertColours()
    {
        invertColoursPlane.SetActive(!invertColoursPlane.activeSelf);
    }

    public static void NextLevel()
    {
        levelMover.levelSpawnPoint = 0;
        playerController.lastCheckpoint = Vector3.zero;
        PlayerDied();
    }

    public static void SetCheckpoint(Checkpoint checkpoint)
    {
        levelMover.levelSpawnPoint = checkpoint.startingPosition.x; //levelMover.transform.position.x;
        playerController.lastCheckpoint = new Vector3(0, checkpoint.transform.position.y, 0);
    }
}
