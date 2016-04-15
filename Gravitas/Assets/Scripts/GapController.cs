using UnityEngine;
using System.Collections;

public class GapController : MonoBehaviour {

    private SpringJoint2D spring;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            if (!spring.enabled)
            {
                if(collider.transform.position.y - transform.position.y > 0 && GameManager.gravityIsDown) //player is above gap and gravity is down
                {
                    print("player is above gap and gravity is down");
                    spring.enabled = true;
                    spring.connectedBody = collider.GetComponent<Rigidbody2D>();
                }
                if (collider.transform.position.y - transform.position.y < 0 && !GameManager.gravityIsDown) //player is below gap and gravity is up
                {
                    print("player is below gap and gravity is up");
                    spring.enabled = true;
                    spring.connectedBody = collider.GetComponent<Rigidbody2D>();
                }
            }
            if ((collider.transform.position - transform.position).magnitude < 0.5)
            {
                spring.enabled = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            spring.enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position, transform.localScale * 2);
    }
}
