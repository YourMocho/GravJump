using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelMover : MonoBehaviour {

    public float Xspeed = 5;
    private Vector2 movementVector;
    private Rigidbody2D rigidbody;
    public float levelSpawnPoint = 0;
    //public bool moving = true;

    void Start () {
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
}
