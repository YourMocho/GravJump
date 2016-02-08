﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private bool isTouchingFloor = false;
    private Vector3 playerSpawnPoint;
    public float leftBoundary;
    private float upperBoundary;
    private float lowerBoundary;
    private SpriteRenderer renderer;
    private LevelMover levelMover;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        playerSpawnPoint = transform.position;

        renderer = GetComponent<SpriteRenderer>();

        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();

        leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - renderer.bounds.size.x / 2;
        upperBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y + renderer.bounds.size.y / 2;
        lowerBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - renderer.bounds.size.y / 2;

        print(leftBoundary + "ffffffffffffff");
    }
	
	// Update is called once per frame
	void Update () {
	    if((Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isTouchingFloor && !GameManager.paused)
        {
            rigidbody.gravityScale *= -1;
        }

        //moving off screen --> death
        if(transform.position.x < leftBoundary || transform.position.y > upperBoundary || transform.position.y < lowerBoundary) {
            print("player has died");
            GameManager.ResetMapToSpawnPoint();
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        GameManager.StartCountdown();
        transform.position = playerSpawnPoint;
        rigidbody.velocity = Vector3.zero;
        if (rigidbody.gravityScale < 0)
        {
            rigidbody.gravityScale *= -1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "JumpableFloor")
        {
            isTouchingFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "JumpableFloor")
        {
            isTouchingFloor = false;
        }

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }


}
