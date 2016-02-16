using UnityEngine;
using System.Collections;

public class ColliderCreater : MonoBehaviour {

    public float sizeDifference = 1f;

    void Awake()
    {
        BoxCollider2D thisCollider = GetComponent<BoxCollider2D>();
        GameObject gravTrigger = transform.GetChild(0).gameObject;

        gravTrigger.GetComponent<BoxCollider2D>().size = new Vector2(thisCollider.size.x + sizeDifference / transform.localScale.x, thisCollider.size.y + sizeDifference / transform.localScale.y);
    }
}
