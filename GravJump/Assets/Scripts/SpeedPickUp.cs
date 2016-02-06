using UnityEngine;
using System.Collections;

public class SpeedPickUp : MonoBehaviour {

    public float speedChange = 5;

    void OnTriggerEnter2D(Collider2D collider)
    {
        transform.parent.GetComponent<LevelMover>().ChangeSpeed(speedChange);
        Destroy(gameObject);
    }
}
