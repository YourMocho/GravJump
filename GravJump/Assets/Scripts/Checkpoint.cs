using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject flag;
    private GameObject pole;
    public Vector3 startingPosition;

    public bool upsideDown;

    private TextMesh respawnText;
    public int respawnNumber; //make private

    public void Setup()
    {
       // print("checkpoint awake");
        CheckUpsideDown();

        pole = transform.GetChild(0).gameObject;
        flag = transform.GetChild(1).gameObject;
        flag.SetActive(false);

        respawnNumber = GameManager.checkpointRespawns;
    }

    void Start()
    {
        float x = GameManager.levelMover.transform.position.x - transform.position.x;

        startingPosition = new Vector3(x, transform.position.y, 0);

        if (transform.parent.tag != "TutorialPiece")
        {
            CreateFlagText();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player") && !flag.activeSelf)
        {
            flag.SetActive(true);
            GameManager.SetCheckpoint(this);
        }
    }

    public void UpdateRespawnNumber()
    {
        if (transform.parent.tag != "TutorialPiece")
        {
            print("respawn number before: " + respawnNumber);
            respawnNumber--;

            if (respawnNumber < 0)
            {
                GameManager.GameOver();
            }
            else
            {
                respawnText.text = respawnNumber.ToString();
            }
        }
    }

    public void CreateFlagText()
    {
        GameObject tmpText = new GameObject("RespawnNumberText");
        tmpText.transform.parent = flag.transform;
        tmpText.transform.localPosition = new Vector3(-0.5f, 3, -1);
        tmpText.AddComponent<TextMesh>();

        respawnText = tmpText.GetComponent<TextMesh>();
        respawnText.fontSize = 100;
        respawnText.color = Color.black;
        respawnText.anchor = TextAnchor.MiddleLeft;
        respawnText.characterSize = 0.05f;
        respawnText.text = respawnNumber.ToString();
    }

    public void CheckUpsideDown()
    {
        if (transform.localScale.y < 0)
        {
            upsideDown = true;
        }
        else
        {
            upsideDown = false;
        }
       // print("checking upside down: " + name + " ---> " + upsideDown);
    }

    public void SetVisibility(bool state)
    {
        if(pole == null)
        {
            pole = transform.GetChild(0).gameObject;
        }
        pole.SetActive(state);
        GetComponent<Collider2D>().enabled = state;
    }
}
