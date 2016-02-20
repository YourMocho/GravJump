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

        float x = 0;
        float y = 0;

        if (gameObject.name.Equals("PauseButton"))
        {
            x = Screen.width + (thisTransform.anchoredPosition.x * canvas.scaleFactor) - ((thisTransform.rect.width / 2) * canvas.scaleFactor);
            y = Screen.height + (thisTransform.anchoredPosition.y * canvas.scaleFactor) - ((thisTransform.rect.height / 2) * canvas.scaleFactor);
        }
        if (gameObject.name.Equals("BackButton"))
        {
            x = (thisTransform.anchoredPosition.x * canvas.scaleFactor) + ((thisTransform.rect.width / 2) * canvas.scaleFactor);
            y = Screen.height + (thisTransform.anchoredPosition.y * canvas.scaleFactor) - ((thisTransform.rect.height / 2) * canvas.scaleFactor);
        }

        Vector2 buttonCenter = new Vector2(x, y);

        if ((eventData.position - buttonCenter).magnitude < (thisTransform.rect.width / 2) * canvas.scaleFactor) //inside the circle button
        {
            if (gameObject.name.Equals("PauseButton"))
            {
                GameManager.TogglePause();
                GetComponent<PauseButton>().UpdateSprite();
            }
            if (gameObject.name.Equals("BackButton"))
            {
                SceneManager.LoadScene("Menu");
            }

        } else //outside of it, so change gravity
        {
            GameManager.playerController.ChangeGravity();
        }
    }
}
