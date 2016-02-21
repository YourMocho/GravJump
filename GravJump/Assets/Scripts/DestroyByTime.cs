using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float time;
    private float timer;

    void Start()
    {
        timer = Time.time;
    }

	void Update () {
	    if(timer + time < Time.deltaTime)
        {
            Destroy(this.gameObject);
        }
	}
}
