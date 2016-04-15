using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ClickPanelInput : MonoBehaviour, IPointerDownHandler {

    public void OnPointerDown(PointerEventData eventData)
    {
        print("CLICK");
        GameManager.playerController.ChangeGravity();
    }
}
