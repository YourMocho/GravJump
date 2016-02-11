using UnityEngine;
using System.Collections;

public class GravChangePickUp : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D collider)  
    {
        if(collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().numberOfColliders++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().numberOfColliders--;
        }
    }
}
