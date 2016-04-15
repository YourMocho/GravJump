using UnityEngine;
using System.Collections;

public class GravChangePickUp : MonoBehaviour {

    private Color startColour;
    public bool changeColourWhenActive = false;

    void Start()
    {
        startColour = GetComponent<SpriteRenderer>().color;
    }

    void OnTriggerEnter2D(Collider2D collider)  
    {
        if(collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().numberOfColliders++;
            if (changeColourWhenActive)
            {
                 GetComponent<SpriteRenderer>().color = new Color(238 / 255f, 159 / 255f, 152 / 255f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            collider.gameObject.GetComponent<PlayerController>().numberOfColliders--;
            if (changeColourWhenActive)
            {
                GetComponent<SpriteRenderer>().color = startColour;
            }
        }
    }
}
