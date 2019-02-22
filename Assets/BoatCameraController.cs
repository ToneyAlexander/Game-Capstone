using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCameraController : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.RotateAround(Vector3.zero, Vector3.up, h*Time.deltaTime);


    }
}
