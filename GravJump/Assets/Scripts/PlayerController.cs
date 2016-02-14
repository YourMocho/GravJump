using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    public Vector3 playerSpawnPoint;
    public float leftBoundary;
    private float upperBoundary;
    private float lowerBoundary;
    private SpriteRenderer renderer;
    public int numberOfColliders = 0;

    void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        playerSpawnPoint = transform.position;

        renderer = GetComponent<SpriteRenderer>();

        leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - (renderer.bounds.size.x / 2) * GameManager.canvas.scaleFactor;
        upperBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y + (renderer.bounds.size.y / 2) * GameManager.canvas.scaleFactor;
        lowerBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - (renderer.bounds.size.y / 2) * GameManager.canvas.scaleFactor;
    }

    void Update() {
        /*
	    if((Input.GetButtonDown("Fire1") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && numberOfColliders > 0 && GameManager.gameStarted && !GameManager.paused)
        {
            rigidbody.gravityScale *= -1;
            GameManager.invertColoursPlane.SetActive(!GameManager.invertColoursPlane.activeSelf);
        }
        */

        //moving off screen --> death
        if (transform.position.x < leftBoundary || transform.position.y > upperBoundary || transform.position.y < lowerBoundary) {
            print("player has died");
            GameManager.PlayerDied();
            RespawnPlayer();
        }

    }

    void FixedUpdate() { 
       rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }

    public void ChangeGravity()
    {
        if(numberOfColliders > 0 && GameManager.gameStarted && !GameManager.paused)
        {
            rigidbody.gravityScale *= -1;
            GameManager.invertColoursPlane.SetActive(!GameManager.invertColoursPlane.activeSelf);
        }
    }

    private void RespawnPlayer()
    {
        GameManager.invertColoursPlane.SetActive(false);
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
           //numberOfColliders++;
            //collision.collider.GetComponent<SpriteRenderer>().color = GameManager.touchingColour;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "JumpableFloor")
        {
            //numberOfColliders--;
            //collision.collider.GetComponent<SpriteRenderer>().color = GameManager.normalColour;
        }
    }



    private void InvertAllMaterialColors()
    {
        Renderer[] renderers = GameObject.FindObjectsOfType<Renderer>();
        foreach (Renderer render in renderers)
        {
            if (render.material.HasProperty("_Color"))
            {
                render.material.color = InvertColor(render.material.color);
            }
        }
    }

    private Color InvertColor(Color c) {
         return new Color(1.0f-c.r, 1.0f-c.g, 1.0f-c.b);
    }


}
