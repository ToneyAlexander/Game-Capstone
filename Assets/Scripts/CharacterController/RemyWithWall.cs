using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyWithWall : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("撞墙");

            //transform.position = RemyController.lastPosition;

            RemyMovement.destination = transform.position;

        }
        else if (collision.gameObject.CompareTag("Prop"))
        {
            RemyMovement.destination = transform.position;
        }
    }
}
