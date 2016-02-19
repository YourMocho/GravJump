using UnityEngine;
using System.Collections;

public class LevelPiece : MonoBehaviour {

    public bool startDirectionUp;
    public bool endDirectionUp;
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
