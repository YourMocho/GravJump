using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LevelPiece : MonoBehaviour {

    /*
        1 - Top
        2 - Both
        3 - Bottom
    */

    public int startDirection;
    public int endDirection;
    public Transform endPos;

    public bool upsideDown = false;

    public Checkpoint checkpoint; //make private

    public List<InvisibleBlockController> invisibleBlocks;

    void Awake () {
        endPos = transform.FindChild("EndPos");
        if (!tag.Equals("StartPiece"))
        {
            checkpoint = transform.FindChild("Checkpoint").GetComponent<Checkpoint>();
            checkpoint.SetVisibility(false);
        }

        invisibleBlocks = new List<InvisibleBlockController>();

        FindInvisibleBlocks();

    }

    void Start()
    {
        //FlipHorizontal();
        SetupInvisibleBlocks();
    }

    public void MakeCheckpoint()
    {
        if (!tag.Equals("StartPiece"))
        {
            checkpoint.SetVisibility(true);
        }
    }

    public void FlipHorizontal()
    {
        upsideDown = true;
        checkpoint.CheckUpsideDown();
        checkpoint.upsideDown = !checkpoint.upsideDown;

        transform.localScale = new Vector3(1, transform.localScale.y * -1, 1);
        
        if(startDirection == 1)
        {
            startDirection = 3;
        } else if (startDirection == 3)
        {
            startDirection = 1;
        }

        if (endDirection == 1)
        {
            endDirection = 3;
        }
        else if (endDirection == 3)
        {
            endDirection = 1;
        }

        foreach(InvisibleBlockController invBlock in invisibleBlocks)
        {
            //print("flipping blocks");
           invBlock.Flip();
        }
    }

    private void FindInvisibleBlocks()
    {
        InvisibleBlockController[] invBlocks = transform.GetComponentsInChildren<InvisibleBlockController>();

        invisibleBlocks.AddRange(invBlocks.ToList());
    }

    public void FlipInvisibleBlocks()
    {
        foreach(InvisibleBlockController invBlock in invisibleBlocks)
        {
            invBlock.Flip();
        }
    }

    private void SetupInvisibleBlocks()
    {
        if(!GameManager.gravityIsDown)
        {
            FlipInvisibleBlocks();
        }
    }

}
