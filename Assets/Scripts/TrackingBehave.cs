using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBehave : MonoBehaviour
{
    public GameObject Target;
    public float RotSpeed;
    public bool pause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pause)
        {
            var lookPos = Target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Mathf.Min(RotSpeed * Time.deltaTime, 1f));
        }
    }
}
