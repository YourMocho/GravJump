using UnityEngine;
using System.Collections;

public class PlayerResetPickUp : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.ResetPlayer();
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
