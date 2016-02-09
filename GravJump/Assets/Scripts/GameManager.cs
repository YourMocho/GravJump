using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    private static Text countdownText;
    private static LevelMover levelMover;
    public static PlayerController player;
    public static GameObject invertColoursPlane;
    private static Text scoreText;

    private static int score = 0;
    private static int countdown;
    private static float timer;
    public static bool gameStarted;
    public static bool paused = false;
    public static int maxSpeed = 15;
    public static int minSpeed = 3;

    public static Color normalColour; 
    public static Color touchingColour = new Color(67 / 255f,154 / 255f, 212 / 255f);

    private Vector2 gravity = new Vector2(0.0f, -9.8f);

    void Start () {
        normalColour = GameObject.Find("GroundBlock").GetComponent<SpriteRenderer>().color;
        invertColoursPlane = GameObject.Find("InvertColoursPlane");
        invertColoursPlane.SetActive(false);
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreText.text = "";
        StartCountdown();
    }
	
	void Update () {
        if (countdown > 0)
        {
            countdownText.text = countdown.ToString();

            if (Time.time - timer > 1.0f)
            {
                countdown--;
                timer = Time.time;
            }
        }
        if (countdown == 0)
        {
            countdownText.gameObject.SetActive(false);
            gameStarted = true;
            Physics2D.gravity = gravity;
            countdown = -1;
        }

        calculateScore();

        if (score != 0)
        {
            scoreText.text = score.ToString();
        }

        print(GameObject.Find("EventSystem").GetComponent<EventSystem>().IsPointerOverGameObject());
    }

    public static void StartCountdown()
    {
        gameStarted = false;
        Physics2D.gravity = Vector2.zero;
        countdownText.gameObject.SetActive(true);
        countdown = 3;
        timer = Time.time;
    }

    public static void PlayerDied()
    {
        if (PlayerPrefs.GetInt("score") < score)
        {
            PlayerPrefs.SetInt("score", score);
        }
        ResetMapToSpawnPoint();
    }

    public static void ResetMapToSpawnPoint()
    {
        levelMover.transform.position = new Vector3(levelMover.levelSpawnPoint, 0, 0);
    }

    private void calculateScore()
    {
        score = (int)(levelMover.transform.position.x * -1);
    }

    public void TogglePause()
    {
        paused = !paused;
       // print(paused);
    }
}
