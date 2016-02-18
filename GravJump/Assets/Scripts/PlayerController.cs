using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    public Vector3 lastCheckpoint = Vector3.zero;
    public float leftBoundary;
    private float upperBoundary;
    private float lowerBoundary;
    private SpriteRenderer renderer;
    public int numberOfColliders = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - (renderer.bounds.size.x / 2) * GameManager.canvas.scaleFactor;
        upperBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y + (renderer.bounds.size.y / 2) * GameManager.canvas.scaleFactor;
        lowerBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - (renderer.bounds.size.y / 2) * GameManager.canvas.scaleFactor;
    }

    void Update()
    {
        //moving off screen --> death
        if (transform.position.x < leftBoundary || transform.position.y > upperBoundary || transform.position.y < lowerBoundary) {
            GameManager.PlayerDied();
        }

       // print(numberOfColliders);
    }

    void FixedUpdate() { 
       rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }

    public void RemoveAllVelocity()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public void ChangeGravity()
    {
        if(numberOfColliders > 0 && GameManager.gameStarted && !GameManager.paused)
        {
            rigidbody.gravityScale *= -1;
            GameManager.InvertColours();
            GameManager.FlipInvisibleBlocks();
        }
    }

    public void ResetGravityDirectionAndColours()
    {
        if (rigidbody.gravityScale < 0)
        {
            rigidbody.gravityScale *= -1;
            GameManager.InvertColours();
            GameManager.FlipInvisibleBlocks();
        }
    }

    public void RespawnPlayer()
    {
        ResetGravityDirectionAndColours();
        RemoveAllVelocity();
        transform.position = lastCheckpoint;
        GameManager.StartCountdown();
    }

}
