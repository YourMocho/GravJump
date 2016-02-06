using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelMover : MonoBehaviour {

   // private Transform transform;
    public float Xspeed = 5;
    private Vector2 movementVector;
    private Rigidbody2D rigidbody;
    public bool moving = true;

    void Start () {
        movementVector = new Vector2(Xspeed, 0);
        rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (moving)
        {
            rigidbody.MovePosition(rigidbody.position - movementVector * Time.deltaTime);
        } 
    }

    public void ChangeSpeed(float speed)
    {
        movementVector.x += speed;
    }
}
