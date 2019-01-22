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

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        scale = transform.localScale;

        // Set default velocity and speed
        velocity = new Vector3(0.0f, 0.0f, 1.0f);
        speed = 5.0f;

        // Get ball game object
        ball = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        Move();
    }

    public abstract void Move();
    
}
