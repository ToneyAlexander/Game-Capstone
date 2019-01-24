using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPaddle : PaddleAI
{
    public override void Move()
    {
        if (ball.transform.position.z > upperBound)
        {
            transform.position = new Vector3(position.x, position.y, upperBound);
        } 
        else if (ball.transform.position.z < lowerBound)
        {
            transform.position = new Vector3(position.x, position.y, lowerBound);
        }
        else
        {
            transform.position = new Vector3(position.x, position.y, ball.transform.position.z);
        }
    }
}
