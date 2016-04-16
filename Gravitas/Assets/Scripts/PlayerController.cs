using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    private new Rigidbody2D rigidbody;
    public Vector3 spawnPoint = Vector3.zero;
    public float leftBoundary;
    private float upperBoundary;
    private float lowerBoundary;
    private new SpriteRenderer renderer;
    public int numberOfColliders;

    public bool useTrail;
    public GameObject trailObject;
    private List<GameObject> trail;

    private GameObject deathParticles;
    private bool isAlive;
    private bool displayingDeathParticles;
    private float timer;

    void Awake()
    {
        if (useTrail)
        {
            trail = new List<GameObject>();
        }
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        deathParticles = GameObject.Find("DeathParticles");
        ShowDeathParticles(false);

        numberOfColliders = 0;

        useTrail = false;

        isAlive = true;
        displayingDeathParticles = false;
        timer = 0;
    }

    void Start()
    {
        leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - (renderer.bounds.size.x / 2f);
        upperBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y + (renderer.bounds.size.y / 2f);
        //print(upperBoundary + " : " + (renderer.bounds.size.y / 2f) + " : " + Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0)).y);
        lowerBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - (renderer.bounds.size.y / 2f);
    }

    void Update()
    {
        //moving off screen --> death
        if ((transform.position.x < leftBoundary || transform.position.y > upperBoundary || transform.position.y < lowerBoundary) && isAlive && !GameManager.gameOver) {
            //GameManager.PlayerDied();
            print("player has died");
            isAlive = false;
            GameManager.gameStarted = false;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            renderer.enabled = false;
            GameManager.pauseButton.SetActive(false);

            if (GameManager.lastCheckpoint != null)
            {
                GameManager.lastCheckpoint.UpdateRespawnNumber();
            }
        }
        if(!isAlive && !displayingDeathParticles)
        {
            ShowDeathParticles(true);
            timer = Time.time + 0.5f;
        }
        if(displayingDeathParticles)
        {
            if(timer - Time.time < 0 && !GameManager.gameOver)
            {
                print("not dead anymore??");
                ShowDeathParticles(false);

                isAlive = true;
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                renderer.enabled = true;

                GameManager.PlayerDied();
                GameManager.pauseButton.SetActive(true);
            }
        }

        // print(numberOfColliders);
        if (useTrail)
        {
            CreateTrail();
     
            Vector3 trailDir = new Vector3(GameManager.levelMover.Xspeed, rigidbody.velocity.y, 0) + transform.position;
            //print(trailDir);
            transform.GetChild(4).transform.LookAt(trailDir);
        }
    }

    private void CreateTrail()
    {
        GameObject tmp = Instantiate(trailObject, transform.position, Quaternion.identity) as GameObject;
        tmp.transform.parent = GameManager.levelMover.transform;
        trail.Add(tmp);
        if(trail.Count > 10)
        {
            Destroy(trail[0]);
            trail.RemoveAt(0);
        }
        foreach(GameObject t in trail)
        {
            t.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, t.GetComponent<SpriteRenderer>().color.a - 0.1f);
            t.transform.localScale = new Vector3(t.transform.localScale.x - 0.02f, t.transform.localScale.y - 0.02f, t.transform.localScale.z - 0.02f);
        }
    }

    void FixedUpdate() {
        if (!GameManager.playerIsReseting)
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        }
    }

    public void RemoveAllVelocity()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public void ChangeGravity()
    {
        if(numberOfColliders > 0 && GameManager.gameStarted && !GameManager.paused)
        {
            ActuallyChangeGravity();
        }
    }

    private void ActuallyChangeGravity()
    {
        rigidbody.gravityScale *= -1;
        GameManager.InvertColours();
        GameManager.FlipInvisibleBlocks();
        if (rigidbody.gravityScale < 0)
        {
            GameManager.gravityIsDown = false;
        } else
        {
            GameManager.gravityIsDown = true;
        }
        //print(GameManager.gravityIsDown);
    }

    public void ResetGravityDirectionAndColours()
    {
        if (GameManager.lastCheckpoint != null)
        {
            if ((GameManager.lastCheckpoint.upsideDown && GameManager.gravityIsDown) || (!GameManager.lastCheckpoint.upsideDown && !GameManager.gravityIsDown)) //set grav and colour invert depending on checkpoint orientation
            {
                ActuallyChangeGravity();
            }
        } else
        {
            if(rigidbody.gravityScale < 0)
            {
                ActuallyChangeGravity();
            }
        }
    }

    public void RespawnPlayer()
    {
        ResetGravityDirectionAndColours();
        RemoveAllVelocity();
        transform.position = spawnPoint;
    }

    //////////////

    public void ShowDeathParticles(bool state)
    {
        deathParticles.SetActive(state);
        if (transform.position.x < leftBoundary) 
        {
            //0,90,0
            deathParticles.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if(transform.position.y > upperBoundary) {
            //90, 0,0
            deathParticles.transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if (transform.position.y < lowerBoundary)
        {
            //-90,0,0
            deathParticles.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        displayingDeathParticles = state;
    }

}
