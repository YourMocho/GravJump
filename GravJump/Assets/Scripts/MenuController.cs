using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private Text highscoreText;
    private GameObject menuPlayer;
    public GameObject invertCircle; 
    private GameObject StartButton;
	
	void Awake () {
        menuPlayer = GameObject.Find("MenuPlayer");
        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        StartButton = GameObject.Find("StartGameButton");
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("score").ToString();
    }

    public void StartGame()
    {
        Physics2D.gravity = new Vector2(0.0f, -9.8f); ;
        print(menuPlayer.GetComponent<Rigidbody2D>().gravityScale);
        menuPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        Instantiate(invertCircle, menuPlayer.transform.position, invertCircle.transform.rotation);
        StartButton.GetComponent<Button>().enabled = false;
        Invoke("LoadLevel", 1f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
