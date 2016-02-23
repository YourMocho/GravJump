using UnityEngine;
using System.Collections;

public class InvisibleBlockController : MonoBehaviour {

    private new SpriteRenderer renderer;
    private new BoxCollider2D collider;

    public bool invertedBlock;
    public bool visible = true;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        if (invertedBlock)
        {
            Flip();
        }

        renderer.color = GameManager.blockColour;
    }

    public void Hide()
    {
        if (collider.IsTouching(GameManager.playerJumpTrigger))
        {
            GameManager.playerController.numberOfColliders--;
        }

        renderer.enabled = false;
        collider.enabled = false;

        visible = false;
    }

    public void Show()
    {
        renderer.enabled = true;
        collider.enabled = true;

        visible = true;
    }

    public void Flip()
    {
        if(renderer == null || collider == null)
        {
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();
        }
        if (renderer.enabled)
        {
            Hide();
        } else {
            Show();
        }
    }

}
