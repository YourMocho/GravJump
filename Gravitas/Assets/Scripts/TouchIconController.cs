using UnityEngine;
using System.Collections;

public class TouchIconController : MonoBehaviour {

    float rightBoundary;

	void Start () {
        rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 5;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKey && transform.position.x < rightBoundary)
        {
            Destroy(this.gameObject);
        }
	}
}
