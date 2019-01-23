using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    const float basespeed = 8f;
    const float maxSpeed = 25f;
    const float speedGain = 1.05f;
    const float size = 10.3f;
    Vector3 savedVelocity;
    Rigidbody rb;
    PongController pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GameObject.Find("LeftScore").GetComponent<PongController>();
        ResetBall();
    }

    void ResetBall()
    {
        transform.position = new Vector3(0, 1, 0);
        if (0.5f < Random.Range(0f, 1f))
        {
            rb.velocity = Vector3.Normalize(new Vector3(Random.Range(0.2f, 1f), 0, Random.Range(-1f, 1f))) * basespeed;
        } else
        {
            rb.velocity = Vector3.Normalize(new Vector3(Random.Range(-0.2f, -1f), 0, Random.Range(-1f, 1f))) * basespeed;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (getPa)
        
        if (GameSystem.getPaused())
        {
            rb.isKinematic = true ;
        }
        else
        {
            
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
                rb.AddForce(savedVelocity, ForceMode.VelocityChange);
            }
            else
            {
                savedVelocity = rb.velocity;
            }
            
            
            
        }
        if (transform.position.x > size)
        {
            ++pc.rightScore;
            ResetBall();
        }
        if (transform.position.x < -size)
        {
            ++pc.leftScore;
            ResetBall();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity = rb.velocity * speedGain;
        } else
        {
            rb.velocity = Vector3.Normalize(rb.velocity) * maxSpeed;
        }

        // Ball changes to a random color on collision.
        Color newColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
    } 
   
}
