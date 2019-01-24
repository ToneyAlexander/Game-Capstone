using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyPaddle : PaddleAI
{
    public override void Move()
    {
        speed = 5.0f;

        // LazyPaddle moves only if the ball is coming and is not within paddle's range
        if (ball.transform.position.x >= 0) {
            if (ball.transform.position.z > (position.z + scale.z / 2)
            && position.z < upperBound)
            {
                transform.Translate(velocity * speed * Time.deltaTime);
            }
            else if (ball.transform.position.z < (position.z - scale.z / 2)
            && position.z > lowerBound)
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
