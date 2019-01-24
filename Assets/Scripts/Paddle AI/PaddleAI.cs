using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleAI : MonoBehaviour
{
    protected Vector3 position;
    protected Vector3 scale;
    protected Vector3 velocity;
    protected float speed;
    protected GameObject ball;
    protected float upperBound, lowerBound;

    private GameObject southWall, northWall;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        scale = transform.localScale;

        // Set default velocity and speed
        velocity = new Vector3(0.0f, 0.0f, 1.0f);
        speed = 5.0f;

        // Get relative game objects
        ball = GameObject.Find("Sphere");
        southWall = GameObject.Find("South_Wall");
        northWall = GameObject.Find("North_Wall");

        // Get upper and lower bounds
        upperBound = northWall.transform.position.z - scale.z / 2 - northWall.transform.localScale.z / 2;
        lowerBound = southWall.transform.position.z + scale.z / 2 + southWall.transform.localScale.z / 2;
    }

    // Update is called once per frame

    void Update()
    {
       // if (Gam)
        if (!GameSystem.getPaused())
        {
            position = transform.position;
            Move();
        }

    }

    public abstract void Move();
    
}
