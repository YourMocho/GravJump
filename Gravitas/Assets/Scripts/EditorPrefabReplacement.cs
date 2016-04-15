using UnityEngine;
//using UnityEditor;

[ExecuteInEditMode]
public class EditorPrefabReplacement : MonoBehaviour {

    public GameObject groundBlock;
    public GameObject invisibleBlock;
    public GameObject invisibleBlockINVERTED;
    public GameObject checkPoint;
    public GameObject gravityPickUp;
    public GameObject gap;

    void Start () {

        /*
       GameObject[] gBlocks = GameObject.FindGameObjectsWithTag("GroundBlock");

       foreach(GameObject block in gBlocks)
       {
           GameObject tmp = PrefabUtility.InstantiatePrefab(groundBlock) as GameObject;

           tmp.transform.position = block.transform.position;
           tmp.transform.rotation = block.transform.rotation;
           tmp.transform.localScale = block.transform.localScale;
           tmp.transform.parent = block.transform.parent;

           DestroyImmediate(block.gameObject);
       }


       GameObject[] checkPoints = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

       foreach (GameObject cp in checkPoints)
       {
           if (cp.name.Equals("Checkpoint"))
           {
               print("found checkpoint");
               GameObject tmp = PrefabUtility.InstantiatePrefab(checkPoint) as GameObject;

               tmp.transform.position = cp.transform.position;
               tmp.transform.rotation = cp.transform.rotation;
               tmp.transform.localScale = cp.transform.localScale;
               tmp.transform.parent = cp.transform.parent;

               DestroyImmediate(cp.gameObject);
           } else
           {
               print("nope");
           }
       }


       GameObject[] invblocks = GameObject.FindGameObjectsWithTag("InvisibleBlock");

       foreach (GameObject invBlock in invblocks)
       {
           GameObject tmp;
           if (invBlock.GetComponent<InvisibleBlockController>().invertedBlock)
           {
               tmp = PrefabUtility.InstantiatePrefab(invisibleBlockINVERTED) as GameObject;
           } else
           {
               tmp = PrefabUtility.InstantiatePrefab(invisibleBlock) as GameObject;
           }

           tmp.transform.position = invBlock.transform.position;
           tmp.transform.rotation = invBlock.transform.rotation;
           tmp.transform.localScale = invBlock.transform.localScale;
           tmp.transform.parent = invBlock.transform.parent;

           DestroyImmediate(invBlock.gameObject);
       }
       

        GameObject[] gravPickUps = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject gravPickUp in gravPickUps)
        {
            if (gravPickUp.name.StartsWith("GravChangePickUp"))
            {
                GameObject tmp = PrefabUtility.InstantiatePrefab(gravityPickUp) as GameObject;

                tmp.transform.position = gravPickUp.transform.position;
                tmp.transform.rotation = gravPickUp.transform.rotation;
                tmp.transform.localScale = gravPickUp.transform.localScale;
                tmp.transform.parent = gravPickUp.transform.parent;

                DestroyImmediate(gravPickUp.gameObject);
            }
            else
            {
                print("nope");
            }
        }
        

        GameObject[] gapBlocks = GameObject.FindGameObjectsWithTag("Gap");

        foreach (GameObject gapBlock in gapBlocks)
        {
            GameObject tmp = PrefabUtility.InstantiatePrefab(gap) as GameObject;

            tmp.transform.position = gapBlock.transform.position;
            tmp.transform.rotation = gapBlock.transform.rotation;
            tmp.transform.localScale = gapBlock.transform.localScale;
            tmp.transform.parent = gapBlock.transform.parent;

            DestroyImmediate(gapBlock.gameObject);
        }
        */
    }

}
