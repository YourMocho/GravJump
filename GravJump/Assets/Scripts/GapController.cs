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
            //print("connected - " + (collider.transform.position - transform.position).magnitude);
            if (!spring.enabled)
            {
                spring.enabled = true;
                spring.connectedBody = collider.GetComponent<Rigidbody2D>();
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
}
