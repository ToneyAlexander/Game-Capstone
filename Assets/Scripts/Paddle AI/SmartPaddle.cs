using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPaddle : PaddleAI
{
    public override void Move()
    {
        transform.position = new Vector3(position.x, position.y, ball.transform.position.z);
    }
}
