using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private Text highscoreText;
    private GameObject menuPlayer;
    public GameObject invertCircle; 
	
	void Start () {
        menuPlayer = GameObject.Find("MenuPlayer");
        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("score").ToString();
    }

    public void StartGame()
    {
        menuPlayer.GetComponent<Rigidbody2D>().gravityScale *= -1;
        Instantiate(invertCircle, menuPlayer.transform.position, invertCircle.transform.rotation);

        Invoke("LoadLevel", 1f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
