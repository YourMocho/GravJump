using UnityEngine;
using System.Collections;

public class LevelFinish : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.NextLevel();
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
