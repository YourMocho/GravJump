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

    public int difficulty;
    public int startDirection;
    public int endDirection;
    public Transform endPos;

    public bool upsideDown;

    public Checkpoint checkpoint; //make private

    public List<InvisibleBlockController> invisibleBlocks;

    public void Setup () {
        upsideDown = false;

       // print("Awake: " + name);

        endPos = transform.FindChild("EndPos");
        if (!tag.Equals("StartPiece"))
        {
            checkpoint = transform.FindChild("Checkpoint").GetComponent<Checkpoint>();
            checkpoint.SetVisibility(false);

            checkpoint.Setup();
        }

        invisibleBlocks = new List<InvisibleBlockController>();

        FindInvisibleBlocks();


    }

    public void MakeCheckpoint()
    {
        if (!tag.Equals("StartPiece"))
        {
            checkpoint.SetVisibility(true);

            if(GameManager.levelCreator.increaseMaxDifficultyWithEachCheckpoint)
            {
                GameManager.levelCreator.maxDifficulty++;
            }
        }
    }

    public void FlipHorizontal()
    {
        print("flipped piece: " + name);

        upsideDown = true;
        checkpoint.CheckUpsideDown();
        checkpoint.upsideDown = !checkpoint.upsideDown;
        //print("just flipped so im now: " + checkpoint.upsideDown);

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

        FlipInvisibleBlocks();
    }

    private void FindInvisibleBlocks()
    {
        InvisibleBlockController[] invBlocks = transform.GetComponentsInChildren<InvisibleBlockController>();

        invisibleBlocks.AddRange(invBlocks.ToList());

        print("Found " + invBlocks.Length + " Inv blocks: " + name);

        foreach (InvisibleBlockController invBlock in invisibleBlocks)
        {
            invBlock.Setup();
        }
    }

    public void FlipInvisibleBlocks()
    {
        foreach(InvisibleBlockController invBlock in invisibleBlocks)
        {
            invBlock.Flip();
        }
    }
}
