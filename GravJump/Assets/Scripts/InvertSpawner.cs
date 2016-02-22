using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvertSpawner : MonoBehaviour {

    public GameObject invertCircle;
    private List<InvertController> inverters;
	
    void Awake()
    {
        inverters = new List<InvertController>();
    }

    public void Invert()
    {
        GameObject tmp = Instantiate(invertCircle, transform.position, invertCircle.transform.rotation) as GameObject;
        tmp.transform.parent = transform;
        inverters.Add(tmp.GetComponent<InvertController>());
        // tmp.GetComponent<InvertController>().Expand();

        DeletePairs();
    }

    private void DeletePairs()
    {
        for(int i = 1; i < inverters.Count; i++)
        {
            if(inverters[i].expanding == false && inverters[i-1].expanding == false)
            {
                Destroy(inverters[i].gameObject);
                Destroy(inverters[i-1].gameObject);
                inverters.RemoveAt(i);
                inverters.RemoveAt(i-1);
                break;
            }
        }
    }


}
