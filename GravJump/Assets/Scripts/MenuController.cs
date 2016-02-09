using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private Text highscoreText;
	
	void Start () {
        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        highscoreText.text = PlayerPrefs.GetInt("score").ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainLevel");
    }
}
