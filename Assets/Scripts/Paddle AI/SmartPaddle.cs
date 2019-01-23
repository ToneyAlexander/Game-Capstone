using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPaddle : PaddleAI
{
    public override void Move()
    {
        if (ball.transform.position.z > 3.75f)
        {
            transform.position = new Vector3(position.x, position.y, 3.75f);
        } 
        else if (ball.transform.position.z < -3.75f)
        {
            transform.position = new Vector3(position.x, position.y, -3.75f);
        }
        else 
        {
            transform.position = new Vector3(position.x, position.y, ball.transform.position.z);
        }
    }
}
