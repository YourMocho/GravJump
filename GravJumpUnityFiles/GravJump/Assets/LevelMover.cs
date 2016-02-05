using UnityEngine;
using System.Collections;

public class LevelMover : MonoBehaviour {

    private Transform transform;
    public float Xspeed = -500;
    private Vector2 movementVector;
    private Rigidbody2D rigidbody;

    void Start () {
        transform = GetComponent<Transform>();
        movementVector = new Vector2(Xspeed, 0);
        rigidbody = GetComponent<Rigidbody2D>();
       // rigidbody.AddForce(movementVector);
    }

	void Update () {
        // transform.position.x += Xspeed;
        //transform.Translate(Xspeed * Time.deltaTime, 0, 0);
        rigidbody.MovePosition(rigidbody.position + movementVector * Time.deltaTime);
    }
}
