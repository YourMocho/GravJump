using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    public Sprite pauseSprite;
    public Sprite playSprite;

    public void ToggleSprite() {
        if (GameManager.gameStarted)
        {
            if (GetComponent<Image>().sprite == pauseSprite)
            {
                GetComponent<Image>().sprite = playSprite;
            }
            else {
                GetComponent<Image>().sprite = pauseSprite;
            }
        }

    }


}
