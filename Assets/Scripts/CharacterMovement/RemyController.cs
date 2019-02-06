﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyController : MonoBehaviour
{
    public static Vector3 destination;
    public static Vector3 lastPosition;
    public float rotationSpeed;
    public float movingSpeed;
    private Quaternion playerRot;
    private  Animator animator;
    private float timer;
    private float EPSSION;
    // Start is called before the first frame update
    void Start()
    {
        EPSSION = 0.2f;
        timer = 0;
        rotationSpeed = 20;
        movingSpeed = 10;
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        transform.position = destination;
    }

    // Update is called once per frame
    void Update()
    {

        DoNotFlay();
        Rotate();
        Move();
        //lastPosition = transform.position;
        SavePos();

    }

    void Rotate()
    {
        if (destination != new Vector3(0,0,0)) {
            playerRot = Quaternion.LookRotation(destination);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotationSpeed * Time.deltaTime);
        }

    }
    void Move()
    {
        if (transform.position != destination) {
            animator.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, destination, movingSpeed * Time.deltaTime);  
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void DoNotFlay()
    {
        if (destination.y > 0.1f)
        {
            destination.y = 0.1f;
        }
    }

    void SavePos()
    {
        timer += Time.deltaTime;
        if (timer > EPSSION)
        {
            lastPosition = transform.position;
            Debug.Log("last position: " + RemyController.lastPosition);
            Debug.Log("current position: " + transform.position);
            Debug.Log(timer);
            timer = 0;
        }
    }
}
