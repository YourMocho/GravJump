using UnityEngine;
using System.Collections;

public class GroundTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.playerController.numberOfColliders++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.playerController.numberOfColliders--;
        }
    }
}
