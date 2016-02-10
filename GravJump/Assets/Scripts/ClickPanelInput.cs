using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ClickPanelInput : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        print("CLICK");
        GameManager.playerController.ChangeGravity();
    }
}
