using UnityEngine;
using System.Collections;

public class ColliderCreater : MonoBehaviour {

    public float sizeDifference = 1f;
    private BoxCollider2D childCollider;
    private GameObject colliderEmpty;

    void Awake()
    {
        BoxCollider2D thisCollider = GetComponent<BoxCollider2D>();

        colliderEmpty = Instantiate(new GameObject(), transform.position, Quaternion.identity) as GameObject;
        colliderEmpty.transform.parent = transform;
        colliderEmpty.AddComponent<GroundTrigger>();

        childCollider = colliderEmpty.AddComponent<BoxCollider2D>() as BoxCollider2D;
        childCollider.isTrigger = true;

        childCollider.size = new Vector2(thisCollider.size.x * transform.localScale.x + sizeDifference, thisCollider.size.y * transform.localScale.y + sizeDifference);
    }
}
