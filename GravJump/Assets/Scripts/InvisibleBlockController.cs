using UnityEngine;
using System.Collections;

public class InvisibleBlockController : MonoBehaviour {

    private new SpriteRenderer renderer;
    private new BoxCollider2D collider;

    public bool invertedBlock;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

        if (invertedBlock)
        {
            Hide();
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
    }

    public void Show()
    {
        renderer.enabled = true;
        collider.enabled = true;
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
