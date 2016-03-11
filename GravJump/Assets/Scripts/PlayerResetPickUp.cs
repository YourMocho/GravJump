using UnityEngine;
using System.Collections;

public class PlayerResetPickUp : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.playerIsReseting = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

}
