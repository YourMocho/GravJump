﻿using UnityEngine;
using System.Collections;

public class GapController : MonoBehaviour {

    private SpringJoint2D spring;

    void Awake()
    {
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            //print("connected - " + (collider.transform.position - transform.position).magnitude);
            if (!spring.enabled)
            {
                if(collider.transform.position.y - transform.position.y > 0 && collider.GetComponent<Rigidbody2D>().gravityScale > 0) //player is above gap and gravity is down
                {
                    print("player is above gap and gravity is down");
                    spring.enabled = true;
                    spring.connectedBody = collider.GetComponent<Rigidbody2D>();
                }
                if (collider.transform.position.y - transform.position.y < 0 && collider.GetComponent<Rigidbody2D>().gravityScale < 0) //player is below gap and gravity is up
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
}
