using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour {

    

    public List<LevelPiece> allPossibleLevelPieces;
    public LevelPiece startingPiece;

    private List<LevelPiece> topPieces;
    private List<LevelPiece> bottomPieces;
    private List<LevelPiece> bothPieces;

    public List<LevelPiece> currentLevelPieces; //private plssss
    private Transform nextPieceSpawnpoint;
    private float rightBoundary;

    public bool useCheckpointsInsteadOfDifficulty;
    public int difficultyPerCheckpoint;
    public int allowedDifficultyOverflow;
    private int cumulitiveDifficulty = 0;

    public bool useMaxDifficulty;
    public int maxDifficulty;

    private int checkpointCount;
    public int piecesPerCheckpoint;
    public LevelPiece checkpointPiece;

    public GameObject levelPiecesParent;

    public List<int> deathsPerCheckPoint;

    // Use this for initialization
    void Awake () {

        LoadPossibleLevelPieces();

        currentLevelPieces = new List<LevelPiece>();
        topPieces = new List<LevelPiece>();
        bottomPieces = new List<LevelPiece>();
        bothPieces = new List<LevelPiece>();

        deathsPerCheckPoint = new List<int>();

        AssignTopAndBottomPieces();
    }

    private void LoadPossibleLevelPieces()
    {
        GameObject[] p;
        // p = Resources.LoadAll<LevelPiece>("Prefabs/LevelPieces/");
        //allPossibleLevelPieces = p.ToList<LevelPiece>();

        levelPiecesParent.SetActive(true);

        p = GameObject.FindGameObjectsWithTag("LevelPiece");

        foreach(GameObject piece in p)
        {
            allPossibleLevelPieces.Add(piece.GetComponent<LevelPiece>());
            piece.gameObject.SetActive(false);
        }

        //startingPiece = Resources.Load<LevelPiece>("Prefabs/StartPiece");
        startingPiece = GameObject.FindGameObjectWithTag("StartPiece").GetComponent<LevelPiece>();
    }   

    void Start()
    {
        rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x + 1;

        nextPieceSpawnpoint = transform;

        SelectNextPieceWithFlipping();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentLevelPieces[currentLevelPieces.Count - 1].endPos.position.x < rightBoundary)
        {
            SelectNextPieceWithFlipping();
        }

        /*
        if (Input.GetKeyDown(KeyCode.P) || (Input.touchCount > 3))
        {
            string s = "Checkpoint Deaths:  ";
            foreach (int i in deathsPerCheckPoint)
            {
                print(i);
                s += i + ", ";
            }
            GameObject.Find("DebugText").GetComponent<Text>().text = s;
        }
        */
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

        if (useCheckpointsInsteadOfDifficulty)
        {
            print("checkpoints");
            checkpointCount++;
            if (checkpointCount == piecesPerCheckpoint)
            {
                tmpPiece.GetComponent<LevelPiece>().MakeCheckpoint();
                checkpointCount = 0;
            }
        } else
        {
            cumulitiveDifficulty += tmpPiece.GetComponent<LevelPiece>().difficulty;
            print(cumulitiveDifficulty);
            if(cumulitiveDifficulty > difficultyPerCheckpoint)
            {
                print("over dif so made checkpoint");
                tmpPiece.GetComponent<LevelPiece>().MakeCheckpoint();
                cumulitiveDifficulty = 0;
            }
        }

        currentLevelPieces.Add(tmpPiece.GetComponent<LevelPiece>());
    }

    private void SelectNextPieceWithFlipping()
    {
        int randomIndex;
        GameObject tmpPiece = null;

        if (currentLevelPieces.Count > 0)
        {

            randomIndex = (int)Random.Range(0f, allPossibleLevelPieces.Count);

            //print("random NO: " + randomIndex + " out of: " + allPossibleLevelPieces.Count);

            bool foundPiece = false;

            while (!foundPiece)
            {
                if (useMaxDifficulty)
                {
                    //print("using max dif");

                    if(allPossibleLevelPieces[randomIndex].difficulty > maxDifficulty)
                    {
                        //print(allPossibleLevelPieces[randomIndex].difficulty + " was too difficult");

                        randomIndex = (int)Random.Range(0f, allPossibleLevelPieces.Count);
                    } else
                    {
                       // print("below max dif - piece " + allPossibleLevelPieces[randomIndex].name);
                        foundPiece = true;
                    }
                } else
                {
                    foundPiece = true;
                }
                if (foundPiece)
                {
                    if (cumulitiveDifficulty + allPossibleLevelPieces[randomIndex].difficulty > difficultyPerCheckpoint + allowedDifficultyOverflow)
                    {
                       // print("over overflow dif - piece " + allPossibleLevelPieces[randomIndex].name);
                        foundPiece = false;
                        randomIndex = (int)Random.Range(0f, allPossibleLevelPieces.Count);
                    }
                }

            }

            tmpPiece = Instantiate(allPossibleLevelPieces[randomIndex].gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;

            print("instantiated new piece: " +tmpPiece.name);

            tmpPiece.GetComponent<LevelPiece>().Setup();

            if (allPossibleLevelPieces[randomIndex].startDirection == currentLevelPieces[currentLevelPieces.Count - 1].endDirection) //both pieces are 1s or 2s or 3s
            {
     
            }
            if (allPossibleLevelPieces[randomIndex].startDirection == 2 || currentLevelPieces[currentLevelPieces.Count - 1].endDirection == 2) //new piece or old piece is a 2 so new one can flip either way
            {
                //flip 50% of time
                float rand = Random.value;
                if(rand < 0.5)
                {
                    tmpPiece.GetComponent<LevelPiece>().FlipHorizontal();
                }
            }
            if (Mathf.Abs(allPossibleLevelPieces[randomIndex].startDirection - currentLevelPieces[currentLevelPieces.Count - 1].endDirection) == 2) //new piece is opposite of old one so have to flip
            {
                //flip piece
                tmpPiece.GetComponent<LevelPiece>().FlipHorizontal();
            }

        } else
        {
            print("start piece");

            tmpPiece = Instantiate(startingPiece.gameObject, nextPieceSpawnpoint.position, Quaternion.identity) as GameObject;
            tmpPiece.GetComponent<LevelPiece>().Setup();
        }

        tmpPiece.SetActive(true);
        tmpPiece.transform.parent = transform;
        nextPieceSpawnpoint = tmpPiece.GetComponent<LevelPiece>().endPos;

        if (useCheckpointsInsteadOfDifficulty)
        {
            print("checkpoints");
            checkpointCount++;
            if (checkpointCount == piecesPerCheckpoint)
            {
                tmpPiece.GetComponent<LevelPiece>().MakeCheckpoint();
                checkpointCount = 0;
            }
        }
        else
        {
            cumulitiveDifficulty += tmpPiece.GetComponent<LevelPiece>().difficulty;
            print(cumulitiveDifficulty);
            if (cumulitiveDifficulty > difficultyPerCheckpoint)
            {
                print("over dif so made checkpoint");
                tmpPiece.GetComponent<LevelPiece>().MakeCheckpoint();
                cumulitiveDifficulty = 0;
            }
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

    bool first = true;

    public void RecordDeathsAtCheckpoint()
    {
        print("recorded");
        if (!first)
        {
            deathsPerCheckPoint.Add(checkpointPiece.checkpoint.respawnNumber);
        }
        first = false;
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
            Destroy(currentLevelPieces[0].gameObject);
            currentLevelPieces.RemoveAt(0);
        }

        print("removed " + removeIndex + " pieces");
    }

}
