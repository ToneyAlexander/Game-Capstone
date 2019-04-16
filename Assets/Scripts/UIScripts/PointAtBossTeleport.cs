using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtBossTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(GameObject.FindGameObjectWithTag("BossTeleporter").transform);
    }
}
