using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text countdownText;
    private int countdown = 3;
    private float timer;

	void Start () {

	}
	
	void Update () {
        if (countdown > 0)
        {
            countdownText.text = countdown.ToString();
        }
    }
}
