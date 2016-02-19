using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour {

    public LevelPiece[] levelPieces;

    private List<LevelPiece> topPieces;
    private List<LevelPiece> bottomPieces;
    private List<LevelPiece> bothPieces;

    private List<LevelPiece> currentLevel;
    private Transform nextPieceSpawnpoint;
    private float rightBoundary;
    private float leftBoundary;

    // Use this for initialization
    void Awake () {
        currentLevel = new List<LevelPiece>();
        topPieces = new List<LevelPiece>();
        bottomPieces = new List<LevelPiece>();
        bothPieces = new List<LevelPiece>();

        AssignTopAndBottomPieces();
    }

    void Start()
    {
        rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1;
        //leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1;

        nextPieceSpawnpoint = transform;

        SelectNextPiece();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentLevel[currentLevel.Count - 1].endPos.position.x < rightBoundary)
        {
            SelectNextPiece();
        }
    }

    private void SelectNextPiece()
    {
        print("new piece");

        int random;
        GameObject tmpPiece = null;
        if (currentLevel.Count > 0)
        {
            if (currentLevel[currentLevel.Count - 1].endDirection == 1)
            {
                float rand = Random.Range(0,topPieces.Count + bothPieces.Count);
                if (rand < topPieces.Count)
                {
                    random = (int)Random.Range(0, topPieces.Count);
                    tmpPiece = Instantiate(topPieces[random].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
                } else
                {
                    //pick a both piece
                }
            }
            if (currentLevel[currentLevel.Count - 1].endDirection == 2)
            {
                tmpPiece = PickAnyPiece();
            }
            if (currentLevel[currentLevel.Count - 1].endDirection == 3)
            {

                float rand = Random.Range(0, bottomPieces.Count + bothPieces.Count);
                if (rand < bottomPieces.Count)
                {
                    random = (int)Random.Range(0, bottomPieces.Count);
                    tmpPiece = Instantiate(bottomPieces[random].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
                }
                else
                {
                    //pick a both piece
                }
            }
        } else
        {
            tmpPiece = PickAnyPiece();
        }

        currentLevel.Add(tmpPiece.GetComponent<LevelPiece>());
        currentLevel[currentLevel.Count - 1].gameObject.SetActive(true);
        currentLevel[currentLevel.Count - 1].transform.parent = transform;
        nextPieceSpawnpoint = currentLevel[currentLevel.Count - 1].endPos;
    }

    private GameObject PickAnyPiece()
    {
        return Instantiate(levelPieces[(int)Random.Range(0, levelPieces.Length)].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
    }

    public void AssignTopAndBottomPieces()
    {
        foreach(LevelPiece piece in levelPieces)
        {
            if(piece.startDirection == 1)
            {
                topPieces.Add(piece);
            }
            if (piece.startDirection == 2)
            {
                bothPieces.Add(piece);
            }
            if (piece.startDirection == 3)
            {
                bottomPieces.Add(piece);
            }
        }

    }

}
