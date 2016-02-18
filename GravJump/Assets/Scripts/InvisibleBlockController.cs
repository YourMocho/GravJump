using UnityEngine;
using System.Collections;

public class InvisibleBlockController : MonoBehaviour {

    private SpriteRenderer renderer;
    private BoxCollider2D collider;
    private BoxCollider2D trigger;

    public bool invertedBlock = false;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        trigger = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    void Start()
    { 
        if(invertedBlock)
        {
            Hide();
        }
    }

    public void Hide()
    {
        renderer.enabled = false;
        collider.enabled = false;
        trigger.enabled = false;
    }

    public void Show()
    {
        renderer.enabled = true;
        collider.enabled = true;
        trigger.enabled = true;
    }

    public void Flip()
    {
        if (renderer.enabled)
        {
            Hide();
        } else {
            Show();
        }
    }

}
