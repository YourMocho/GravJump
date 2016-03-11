using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public GameObject screensParent;

    public static LevelMover levelMover;
    public static PlayerController playerController;
    public static InvertSpawner invertSpawner;
    public static GameObject pauseButton;
    public static Canvas canvas;
    public static CircleCollider2D playerJumpTrigger;

    private static GameObject pauseScreen;

    public static bool paused;

    public static bool gravityIsDown;

    public static Vector2 gravity;

    public static Color blockColour;

    void Awake()
    {
        print("GM awake");
        screensParent.SetActive(true);

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        invertSpawner = GameObject.Find("InvertSpawner").GetComponent<InvertSpawner>();
        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerJumpTrigger = GameObject.Find("JumpTrigger").GetComponent<CircleCollider2D>();

        pauseScreen = GameObject.Find("PauseScreen");
        pauseButton = GameObject.Find("PauseButton");

        pauseScreen.SetActive(false);

        paused = false;
        gravityIsDown = true;
        gravity = new Vector2(0.0f, -9.8f);
        blockColour = new Color(181 / 255f, 151 / 255f, 85 / 255f);
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerForPickUp();
        }
    }

    public static void StartGame()
    {
        Physics2D.gravity = gravity;
    }

    public static void PlayerDied()
    {
        ResetMapAndPlayer();
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
        foreach (GameObject pickUp in pickUps)
        {
            pickUp.GetComponent<SpriteRenderer>().enabled = true;
            pickUp.GetComponent<CircleCollider2D>().enabled = true;
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

    public static void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            paused = true;
            print("game is paused");
            pauseButton.SetActive(false);
            pauseScreen.SetActive(true);
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            paused = false;
            print("game is resumed");
            pauseButton.SetActive(true);
            pauseScreen.SetActive(false);
            playerController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public static void InvertColours()
    {
        invertSpawner.Invert();
    }

    public static void FlipInvisibleBlocks()
    {
           // levelPiece.FlipInvisibleBlocks(); /////////////////////////implement

    }

    public static void NextLevel()
    {
        levelMover.levelSpawnPoint = new Vector3(0, levelMover.levelSpawnPoint.y, levelMover.levelSpawnPoint.z); ;
        playerController.spawnPoint = Vector3.zero;
        PlayerDied();
    }

}
