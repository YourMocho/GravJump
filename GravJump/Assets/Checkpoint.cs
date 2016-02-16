using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject flag;

    void Awake()
    {
        flag = GameObject.Find("Flag");
        flag.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.SetCheckpoint();
            flag.SetActive(true);
        }
    }
}
