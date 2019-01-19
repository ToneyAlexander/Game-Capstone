using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleAI : MonoBehaviour
{
    protected GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public abstract void Move();
}
