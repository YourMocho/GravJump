using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelMover : MonoBehaviour {

    public float Xspeed = 5;
    private Vector2 movementVector;
    private new Rigidbody2D rigidbody;
    public Vector3 levelSpawnPoint;
    //public bool moving = true;

    void Start () {
        levelSpawnPoint = transform.position;
        movementVector = new Vector2(Xspeed, 0);
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (GameManager.gameStarted && !GameManager.paused)
        {
            rigidbody.MovePosition(rigidbody.position - movementVector * Time.deltaTime);

           // levelSpawnPoint = transform.position.x - GameManager.player.transform.position.x; //point of players death
        } 
    }

    public void ChangeSpeed(float speed)
    {
        if (movementVector.x + speed > GameManager.minSpeed && movementVector.x + speed < GameManager.maxSpeed)
        {
            movementVector.x += speed;
        }
    }

    public void SetSpeed(float speed)
    {
        movementVector.x = speed;
    }
}
