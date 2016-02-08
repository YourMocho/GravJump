using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static Text countdownText;
    private static LevelMover levelMover;
    public static PlayerController player;

    private static int countdown;
    private static float timer;
    public static bool paused;
    public static int maxSpeed = 15;
    public static int minSpeed = 3;

    private Vector2 gravity = new Vector2(0.0f, -9.8f);

    void Start () {
        countdownText = GameObject.Find("CountdownText").GetComponent<Text>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
            paused = false;
            Physics2D.gravity = gravity;
        }        
    }

    public static void StartCountdown()
    {
        paused = true;
        Physics2D.gravity = Vector2.zero;
        countdownText.gameObject.SetActive(true);
        countdown = 3;
        timer = Time.time;
    }

    public static void ResetMapToSpawnPoint()
    {
        levelMover.transform.position = new Vector3(levelMover.levelSpawnPoint, 0, 0);
    }
}
