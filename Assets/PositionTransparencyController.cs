using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTransparencyController : MonoBehaviour
{
    public Vector3 position = new Vector3();
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera.transform.position);
    }
}
