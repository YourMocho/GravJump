using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class MenuController : MonoBehaviour {

    private Text highscoreText;
    private GameObject menuPlayer;
    public GameObject invertCircle; 
    private GameObject StartButton;
    // private bool showTutorial;
    private bool increaseChromatic = false;
    private Camera[] cameras;
    private VignetteAndChromaticAberration chromaticAbberationCamera;

    void Awake () {
        menuPlayer = GameObject.Find("MenuPlayer");
        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        StartButton = GameObject.Find("StartGameButton");
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("score").ToString();
        cameras = Camera.allCameras;
        foreach (Camera c in cameras)
        {
            if (c.name.Equals("Platform Camera Normal"))
            {
                chromaticAbberationCamera = c.GetComponent<VignetteAndChromaticAberration>();
            }
        }
    }

    public void StartGame()
    {
        Invoke("LoadLevel", 1.5f);
        increaseChromatic = true;

        Physics2D.gravity = new Vector2(0.0f, -9.8f); ;
        print(menuPlayer.GetComponent<Rigidbody2D>().gravityScale);
        menuPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        Instantiate(invertCircle, menuPlayer.transform.position, invertCircle.transform.rotation);
        StartButton.GetComponent<Button>().enabled = false;
    }

    void Update()
    {
        if(increaseChromatic)
        {
            chromaticAbberationCamera.chromaticAberration += 15f * Time.deltaTime;
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }

}
