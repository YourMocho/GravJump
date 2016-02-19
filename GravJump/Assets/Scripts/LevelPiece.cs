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

    private GameObject checkpoint;

	void Awake () {
        endPos = transform.FindChild("EndPos");
        checkpoint = transform.FindChild("Checkpoint").gameObject;
        checkpoint.SetActive(false);
	}

    public void MakeCheckpoint()
    {
        checkpoint.SetActive(true);
    }

}
