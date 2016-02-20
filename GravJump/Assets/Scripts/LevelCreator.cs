using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelCreator : MonoBehaviour {

    public List<LevelPiece> allPossibleLevelPieces;
    public LevelPiece startingPiece;

    private List<LevelPiece> topPieces;
    private List<LevelPiece> bottomPieces;
    private List<LevelPiece> bothPieces;

    private List<LevelPiece> currentLevelPieces; //private plssss
    private Transform nextPieceSpawnpoint;
    private float rightBoundary;

    private int checkpointCount;
    public int piecesPerCheckpoint = 1;
    public LevelPiece checkpointPiece;

    // Use this for initialization
    void Awake () {

        LoadPossibleLevelPieces();

        currentLevelPieces = new List<LevelPiece>();
        topPieces = new List<LevelPiece>();
        bottomPieces = new List<LevelPiece>();
        bothPieces = new List<LevelPiece>();

        AssignTopAndBottomPieces();
    }

    private void LoadPossibleLevelPieces()
    {
        LevelPiece[] p = Resources.LoadAll<LevelPiece>("Prefabs/LevelPieces/");
        allPossibleLevelPieces = p.ToList<LevelPiece>();

        startingPiece = Resources.Load<LevelPiece>("Prefabs/StartPiece");
        print(startingPiece);
    }   

    void Start()
    {
        rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1;

        nextPieceSpawnpoint = transform;

        SelectNextPiece();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentLevelPieces[currentLevelPieces.Count - 1].endPos.position.x < rightBoundary)
        {
            SelectNextPiece();
        }
    }

    private void SelectNextPiece()
    {
        float random;
        GameObject tmpPiece = null;

        if (currentLevelPieces.Count > 0)
        {

            if (currentLevelPieces[currentLevelPieces.Count - 1].endDirection == 1)
            {

                print("last piece ended at the TOP");

                random = Random.Range(0,topPieces.Count + bothPieces.Count);
                if (random < topPieces.Count)
                {
                    tmpPiece = CreateNewLevelPieceFromList(topPieces);
                } else
                {
                    tmpPiece = CreateNewLevelPieceFromList(bothPieces);
                }
            }
            if (currentLevelPieces[currentLevelPieces.Count - 1].endDirection == 2)
            {
                print("last piece ended at both sides");

                tmpPiece = CreateNewLevelPieceFromList(allPossibleLevelPieces);
            }
            if (currentLevelPieces[currentLevelPieces.Count - 1].endDirection == 3)
            {

                print("last piece ended at the BOTTOM");

                random = Random.Range(0, bottomPieces.Count + bothPieces.Count);
                if (random < bottomPieces.Count)
                {
                    tmpPiece = CreateNewLevelPieceFromList(bottomPieces);
                }
                else
                {
                    tmpPiece = CreateNewLevelPieceFromList(bothPieces);
                }
            }
        } else
        {
            print("first piece");

            tmpPiece = Instantiate(startingPiece.gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
        }

        tmpPiece.SetActive(true);
        tmpPiece.transform.parent = transform;
        nextPieceSpawnpoint = tmpPiece.GetComponent<LevelPiece>().endPos;

        checkpointCount++;

        if (checkpointCount == piecesPerCheckpoint) 
        {
            tmpPiece.GetComponent<LevelPiece>().MakeCheckpoint();
            checkpointCount = 0;
        }

        currentLevelPieces.Add(tmpPiece.GetComponent<LevelPiece>());
    }

    private GameObject CreateNewLevelPieceFromList(List<LevelPiece> list)
    {
        int rand = (int)Random.Range(0, list.Count);
        return Instantiate(list[rand].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
    }

    private GameObject PickAnyPiece()
    {
        return Instantiate(allPossibleLevelPieces[(int)Random.Range(0, allPossibleLevelPieces.Count)].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
    }

    public void AssignTopAndBottomPieces()
    {
        foreach(LevelPiece piece in allPossibleLevelPieces)
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

    public void RemovePiecesUntilLastCheckpoint()
    {
        int removeIndex = 0;
        for (int i = 0; i < currentLevelPieces.Count; i++)
        {
            if (currentLevelPieces[i].Equals(checkpointPiece))
            {
                removeIndex = i - 1;
                break;
            }
        }

        for (int i = 0; i < removeIndex; i++)
        {
            currentLevelPieces.RemoveAt(0);
        }
    }

}
