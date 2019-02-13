using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = player.transform.position;
        position += cameraPosition;
        transform.position = position;
        
    }
}
