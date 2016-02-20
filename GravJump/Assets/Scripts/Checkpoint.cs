using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject flag;
    public Vector3 startingPosition;
    public bool upsideDown;

    void Start()
    {
        float x = GameManager.levelMover.transform.position.x - transform.position.x;

        startingPosition = new Vector3(x, transform.position.y, 0);
        flag = transform.GetChild(1).gameObject;
        flag.SetActive(false);

        if (transform.localScale.y < 0)
        {
            upsideDown = true;
        } else
        {
            upsideDown = false;
        }
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
