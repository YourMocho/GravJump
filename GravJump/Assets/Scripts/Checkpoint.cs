using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject flag;
    public Vector3 startingPosition;

    void Start()
    {
        float x = GameManager.levelMover.transform.position.x - transform.position.x;

        startingPosition = new Vector3(x, transform.position.y, 0);
        flag = transform.GetChild(1).gameObject;
        flag.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            flag.SetActive(true);
            GameManager.SetCheckpoint(this);
        }
    }
}
