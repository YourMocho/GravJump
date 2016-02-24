using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class UIButtonClickable : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform thisTransform = GetComponent<RectTransform>();

        float x = Screen.width + (thisTransform.anchoredPosition.x * canvas.scaleFactor) - ((thisTransform.rect.width / 2) * canvas.scaleFactor);
        float y = Screen.height + (thisTransform.anchoredPosition.y * canvas.scaleFactor) - ((thisTransform.rect.height / 2) * canvas.scaleFactor);

        x = Screen.width * thisTransform.anchorMax.x + (thisTransform.anchoredPosition.x * canvas.scaleFactor);
        x += (thisTransform.pivot.x * -2 + 1) * ((thisTransform.rect.width / 2) * canvas.scaleFactor);

        y = Screen.height * thisTransform.anchorMax.y + (thisTransform.anchoredPosition.y * canvas.scaleFactor);
        y += (thisTransform.pivot.y * -2 + 1) * ((thisTransform.rect.height / 2) * canvas.scaleFactor);

        Vector2 buttonCenter = new Vector2(x, y);

        if ((eventData.position - buttonCenter).magnitude < (thisTransform.rect.width / 2) * canvas.scaleFactor) //inside the circle button
        {
            if (gameObject.name.Equals("PauseButton"))
            {
                GameManager.PauseGame(true);
            }
            if(gameObject.name.Equals("ResumeButton"))
            {
                GameManager.PauseGame(false);
            }
            if (gameObject.name.Equals("BackButton"))
            {
                GameManager.gameOver = false;
              //  GameManager.ResetMapAndPlayer();
                SceneManager.LoadScene("Menu");
            }
            if (gameObject.name.Equals("PlayAgainButton"))
            {
                GameManager.gameOver = false;
               // GameManager.ResetMapAndPlayer();
                SceneManager.LoadScene("MainLevel");
            }

            } else //outside of it, so change gravity
        {
            GameManager.playerController.ChangeGravity();
        }
    }
}
