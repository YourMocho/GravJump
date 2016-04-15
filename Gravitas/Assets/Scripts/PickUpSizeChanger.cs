using UnityEngine;
using System.Collections;

public class PickUpSizeChanger : MonoBehaviour {

    public float sizeChange;
    public float timeDuration;
    private Vector3 startScale;

	void Start () {
        startScale = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale = startScale + new Vector3(Mathf.Sin(Time.time / timeDuration) * sizeChange, Mathf.Sin(Time.time / timeDuration) * sizeChange, Mathf.Sin(Time.time / timeDuration) * sizeChange);
        
	}
}
