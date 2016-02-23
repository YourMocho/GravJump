﻿using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private GameObject flag;
    private GameObject pole;
    public Vector3 startingPosition;

    public bool upsideDown;

    private TextMesh respawnText;
    public int respawnNumber; //make private

    void Awake()
    {
        CheckUpsideDown();

        pole = transform.GetChild(0).gameObject;
        flag = transform.GetChild(1).gameObject;
        flag.SetActive(false);

        respawnNumber = 0;
    }

    void Start()
    {
        float x = GameManager.levelMover.transform.position.x - transform.position.x;

        startingPosition = new Vector3(x, transform.position.y, 0);

        CreateFlagText();
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
        respawnNumber--;
        respawnText.text = respawnNumber.ToString();
        if(respawnNumber < 0)
        {
            GameManager.GameOver();
        }
    }

    public void CreateFlagText()
    {
        GameObject tmpText = new GameObject("RespawnNumberText");
        tmpText.transform.parent = flag.transform;
        tmpText.transform.localPosition = new Vector3(-0.5f, 3, -1);
        tmpText.AddComponent<TextMesh>();
      //  tmpText.AddComponent<MeshRenderer>();

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
