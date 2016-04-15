using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelMover : MonoBehaviour {

    public float Xspeed;
    private Vector2 movementVector;
    private new Rigidbody2D rigidbody;
    public Vector3 levelSpawnPoint;

    void Start () {
        levelSpawnPoint = transform.position;
        movementVector = new Vector2(Xspeed, 0);
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (GameManager.gameStarted && !GameManager.paused && !GameManager.playerIsReseting)
        {
            rigidbody.MovePosition(rigidbody.position - movementVector * Time.deltaTime);
        } 
    }

    public void SetSpeed(float speed)
    {
        movementVector.x = speed;
    }
}
