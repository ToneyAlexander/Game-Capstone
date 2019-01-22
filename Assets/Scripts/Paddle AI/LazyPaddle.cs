using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyPaddle : PaddleAI
{
    public override void Move()
    {
        speed = 3.0f;

        // LazyPaddle moves only if the ball is coming and is not within paddle's range
        if (ball.transform.position.x >= 0) {
            if (ball.transform.position.z > (position.z + scale.z / 2))
            {
                transform.Translate(velocity * speed * Time.deltaTime);
            }
            else if (ball.transform.position.z < (position.z - scale.z / 2))
            {
                transform.Translate(-1 * velocity * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.zero);
            }
        }
    }
}
