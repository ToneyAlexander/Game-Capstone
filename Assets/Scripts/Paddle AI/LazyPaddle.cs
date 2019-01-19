using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyPaddle : PaddleAI
{
    public override void Move()
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;
        
        if (ball.transform.position.z > (position.z + scale.z / 2))
        {
            transform.position += new Vector3(0f, 0f, 0.05f);
        }
        else if (ball.transform.position.z < (position.z - scale.z / 2))
        {
            transform.position -= new Vector3(0f, 0f, 0.05f);
        }
    }
}
