using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyWithWall : MonoBehaviour
{

    private Collision col;
    private void OnCollisionEnter(Collision collision)
    {
        col = collision;
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Debug.Log("撞墙");

            //transform.position = RemyController.lastPosition;

            RemyController.destination = transform.position;

        }
    }
}
