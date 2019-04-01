using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDelete : MonoBehaviour
{

    public float ttl;

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if(ttl < 0f)
        {
            Destroy(gameObject);
        }
    }
}
