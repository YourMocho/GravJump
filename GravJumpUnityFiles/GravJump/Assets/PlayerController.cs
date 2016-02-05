using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rigidbody;
    private bool isTouchingFloor = false;

	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Jump") && isTouchingFloor)
        {
            rigidbody.gravityScale *= -1;
        }


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
       // rigidbody.angularVelocity = 0f;
    }


}
