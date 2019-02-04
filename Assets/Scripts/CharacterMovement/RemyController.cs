using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyController : MonoBehaviour
{
    public static Vector3 destination;
    public float rotationSpeed;
    public float movingSpeed;
    private Quaternion playerRot;
    static Animator animator;
    private Vector3 differ;
    private const float EPSINON = 0.00001f;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 20;
        movingSpeed = 10;
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        transform.position = destination;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
       
        differ.x = transform.position.x - destination.x;
        differ.y = transform.position.y - destination.y;
        differ.z = transform.position.z - destination.z;

        //if ((differ.x > EPSINON || differ.x < -EPSINON)
        //    || (differ.y > EPSINON || differ.y < -EPSINON)
        //    || (differ.z > EPSINON || differ.z < -EPSINON)
        //) {

        if (transform.position != destination) {
            animator.SetBool("isRunning", true);
            playerRot = Quaternion.LookRotation(destination);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, destination, movingSpeed * Time.deltaTime);  
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
    
}
