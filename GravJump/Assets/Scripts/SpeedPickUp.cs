﻿using UnityEngine;
using System.Collections;

public class SpeedPickUp : MonoBehaviour {

    public float speedChange = 5;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.levelMover.ChangeSpeed(speedChange);
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
