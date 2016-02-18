using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("GroundBlock"))
        {
            GameManager.playerController.numberOfColliders++;
        }
        if (collider.tag.Equals("InvisibleBlock"))
        {
            GameManager.playerController.numberOfColliders++;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("GroundBlock"))
        {
            GameManager.playerController.numberOfColliders--;
        }
        if (collider.tag.Equals("InvisibleBlock"))
        {
            GameManager.playerController.numberOfColliders--;
        }
    }
}
