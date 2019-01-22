using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidPaddle : PaddleAI
{
    public override void Move() 
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        if ((position.z + scale.z / 2) > 6)
        {
            transform.position = new Vector3(position.x, position.y, 6.0f - scale.z/2);
            speed = Random.Range(1.0f, 10.0f);
            velocity = -velocity;
        }
        if ((position.z - scale.z / 2) < -6)
        {
            transform.position = new Vector3(position.x, position.y, -(6.0f - scale.z/2));
            speed = Random.Range(1.0f, 10.0f);
            velocity = -velocity;
        }
    }
}
