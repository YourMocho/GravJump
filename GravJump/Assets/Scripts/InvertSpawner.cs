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
        Vector3 spawnPos;
        if (inverters.Count > 0) {
            if (inverters[inverters.Count - 1].transform.position.z - transform.position.z < -10)
            {
                spawnPos = transform.position;
            }
            else {
                spawnPos = new Vector3(transform.position.x, transform.position.y, inverters[inverters.Count - 1].transform.position.z - 0.5f);
            }
        } else
        {
            spawnPos = transform.position;
        }

        GameObject tmp = Instantiate(invertCircle, spawnPos, invertCircle.transform.rotation) as GameObject;
        tmp.transform.parent = transform;
        inverters.Add(tmp.GetComponent<InvertController>());

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
