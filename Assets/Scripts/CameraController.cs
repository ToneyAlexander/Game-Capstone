using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 cameraPosition;

    private Vector3[] cameras;

    int direction = 0;

    void Start()
    {
        cameras = new Vector3[4];
        float angle = 0;
        for (int i = 0; i < cameras.Length; i++)
        {
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            cameras[i] = new Vector3(cameraPosition.x * cos - cameraPosition.z * sin, cameraPosition.y, cameraPosition.x * sin - cameraPosition.z * cos);
            Debug.Log(cameras[i]);
            angle += Mathf.PI * 2 / cameras.Length;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = (direction + 1) % cameras.Length;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            direction = (direction + 1) % cameras.Length;
        }
        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.transform.position, Vector3.Distance(player.transform.position, transform.position));
        foreach(RaycastHit h in hits)
        {
            Renderer rend = h.transform.GetComponent<Renderer>();
            Debug.Log(h.transform.position);
            if (rend)
            {
                Color col = rend.material.color;
                col.a = 0.3f;
                rend.material.color = col;
               
            }
   
        }
        Vector3 position = player.transform.position;
        position += cameras[direction];
        transform.position = Vector3.MoveTowards(transform.position, position, 30 * Time.deltaTime) ;
        transform.LookAt(player.transform);

    }
}
