using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIButtonClickable : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
       // print("BUTTON");

        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasTransform = GetComponentInParent<RectTransform>();
        RectTransform thisTransform = GetComponent<RectTransform>();

        float x = Screen.width + (thisTransform.anchoredPosition.x * canvas.scaleFactor) - ((thisTransform.rect.width / 2) * canvas.scaleFactor);
        float y = Screen.height + (thisTransform.anchoredPosition.y * canvas.scaleFactor) - ((thisTransform.rect.height / 2) * canvas.scaleFactor);
        
        Vector2 buttonCenter = new Vector2(x, y);

        if((eventData.position - buttonCenter).magnitude < (thisTransform.rect.width / 2) * canvas.scaleFactor) //inside the circle button
        {
            GameManager.TogglePause();
        } else //outside of it, so change gravity
        {
            GameManager.playerController.ChangeGravity();
        }
    }
}
