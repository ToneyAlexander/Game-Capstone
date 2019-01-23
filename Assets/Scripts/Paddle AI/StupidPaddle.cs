using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidPaddle : PaddleAI
{
    public override void Move() 
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        if (position.z > 3.75f)
        {
            transform.position = new Vector3(position.x, position.y, 3.75f);
            speed = Random.Range(1.0f, 10.0f);
            velocity = -velocity;
        }
        if (position.z < -3.75f)
        {
            transform.position = new Vector3(position.x, position.y, -3.75f);
            speed = Random.Range(1.0f, 10.0f);
            velocity = -velocity;
        }
    }
}
