using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        velocity = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A)) { // left
			transform.localPosition += new Vector3(-5, 0, 0) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D)) { // right
			transform.localPosition += new Vector3(5, 0, 0) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W)) { // up
			transform.localPosition += new Vector3(0, 0, 5) * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S)) { // down
			transform.localPosition += new Vector3(0, 0, -5) * Time.deltaTime;
		}
    }

    // void FixedUpdate()
    // {
    //     float moveHorizontal = Input.GetAxis("Horizontal");
    //     float moveVertical = Input.GetAxis("Vertical");

    //     Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    //     velocity = (velocity + movement).normalized;
    //     if (velocity != Vector3.zero) 
    //     {
    //         transform.rotation = Quaternion.LookRotation(velocity);
    //     }

    //     rb.AddForce(movement * 5f);
    // }
}
