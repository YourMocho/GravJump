using UnityEngine;
using System.Collections;

public class SpeedPickUp : MonoBehaviour {

    public float speedChange = 5;

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.levelMover.ChangeSpeed(speedChange);
        Destroy(gameObject);
    }
}
