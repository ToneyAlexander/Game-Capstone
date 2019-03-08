using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public float TargetX, TargetY, TargetZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = new Vector3(TargetX,TargetY,TargetZ);
        }

        GameObject.Find("remy").GetComponent<RemyMovement>().setDetination(new Vector3(TargetX, TargetY, TargetZ));

        GameObject.Find("Main Camera").transform.position = new Vector3(TargetX, TargetY + 16, TargetZ);
    }
}
