using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    public Sprite pauseSprite;
    public Sprite playSprite;

    public void UpdateSprite()
    {
        if (GameManager.paused)
        {
            GetComponent<Image>().sprite = playSprite;
        }
        else {
            GetComponent<Image>().sprite = pauseSprite;
        }

    }


}
