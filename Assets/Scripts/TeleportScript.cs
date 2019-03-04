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
        StatBlock colStat = col.gameObject.GetComponent<StatBlock>();
        if(colStat != null && colStat.Friendly)
        {
            col.gameObject.transform.position = new Vector3(TargetX,TargetY,TargetZ);
        }
    }
}
