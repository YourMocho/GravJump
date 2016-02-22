using UnityEngine;
using System.Collections;

public class InvertController : MonoBehaviour {

    float increase = 15000f;
    private Vector3 targetScale;
    private Vector3 maxScale;
    public bool expanding = true;
    private bool shrinking;

    public bool isExpanded;

    void Awake()
    {
        maxScale = new Vector3(6000, 6000, 6000);
        targetScale = maxScale;
        isExpanded = false;
        expanding = true;
        shrinking = false;

        Expand();
    }


    void Update()
    {
        /*
        if (transform.localScale.magnitude < targetScale.magnitude && expanding)
        {
            transform.localScale += new Vector3(increase, increase, increase) * Time.deltaTime;
        } 
        if(transform.localScale.magnitude > targetScale.magnitude && expanding)
        {
            //print("done expanding");
            expanding = false;
            isExpanded = true;
        }

        if (transform.localScale.magnitude > targetScale.magnitude && shrinking)
        {
            transform.localScale -= new Vector3(increase, increase, increase) * Time.deltaTime;
        }
        if (transform.localScale.magnitude < targetScale.magnitude && shrinking)
        {
            shrinking = false;
            isExpanded = false;
        }
        */

        if (shrinking || expanding)
        {
            if (targetScale.x > transform.localScale.x)
            {
                transform.localScale += new Vector3(increase, increase, increase) * Time.deltaTime;
                if (shrinking)
                {
                    shrinking = false;
                }
            }
            if (targetScale.x < transform.localScale.x)
            {
                transform.localScale -= new Vector3(increase, increase, increase) * Time.deltaTime;
                if (expanding)
                {
                    expanding = false;
                }
            }

        }

    }

    public void Expand()
    {
        targetScale = maxScale;
        expanding = true;
    }

    public void Shrink()
    {
        targetScale = Vector3.zero;
        shrinking = true;
    }

    public void Reverse()
    {
        if (targetScale == maxScale)
        {
            Shrink();
        } else if (targetScale == Vector3.zero)
        {
            Expand();
        }
    }
}
