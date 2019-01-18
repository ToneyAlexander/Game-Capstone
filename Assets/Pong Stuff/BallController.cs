using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    const float basespeed = 8f;
    const float speedGain = 1.05f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-1f, 1f)) * basespeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        rb.velocity = rb.velocity * speedGain;
    }
   
}
