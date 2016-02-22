using UnityEngine;
using System.Collections;

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

	void Awake () {
        endPos = transform.FindChild("EndPos");
        if (!tag.Equals("StartPiece"))
        {
            checkpoint = transform.FindChild("Checkpoint").GetComponent<Checkpoint>();
            checkpoint.SetVisibility(false);
        }
    }

    void Start()
    {
        //FlipHorizontal();
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

        GameObject[] invisibleBlocks = GameObject.FindGameObjectsWithTag("InvisibleBlock");

        foreach(GameObject block in invisibleBlocks)
        {
            block.GetComponent<InvisibleBlockController>().Flip();
        }
    }
}
