using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPaddle : PaddleAI
{
    public override void Move()
    {
        Vector3 position = transform.position;

        transform.position = new Vector3(position.x, position.y, ball.transform.position.z);
    }
}
