using UnityEngine;
using System.Collections;

public class ResetStopper : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameManager.playerIsReseting && collider.tag.Equals("GroundBlock"))
        {
            GameManager.playerIsReseting = false;
        }
    }
}
