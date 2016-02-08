using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private bool isTouchingFloor = false;
    private Vector3 spawnPoint;
    private float leftBoundary;
    private float upperBoundary;
    private float lowerBoundary;
    private SpriteRenderer renderer;
    private LevelMover levelMover;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;

        renderer = GetComponent<SpriteRenderer>();

        levelMover = GameObject.Find("LevelAnchor").GetComponent<LevelMover>();

        leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - renderer.bounds.size.x / 2;
        upperBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y + renderer.bounds.size.y / 2;
        lowerBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - renderer.bounds.size.y / 2;

    }
	
	// Update is called once per frame
	void Update () {
	    if((Input.GetButtonDown("Fire1") || Input.touchCount > 0) && isTouchingFloor && !GameManager.paused)
        {
            rigidbody.gravityScale *= -1;
        }

        //moving off screen --> death
        if(transform.position.x < leftBoundary || transform.position.y > upperBoundary || transform.position.y < lowerBoundary) {
            print("player has died");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        transform.position = spawnPoint;
        rigidbody.velocity = Vector3.zero;
        if (rigidbody.gravityScale < 0)
        {
            rigidbody.gravityScale *= -1;
        }

        GameManager.StartCountdown();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "JumpableFloor")
        {
            isTouchingFloor = true;
            print("Touching");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "JumpableFloor")
        {
            isTouchingFloor = false;
            print("Not Touching");
        }

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }


}
