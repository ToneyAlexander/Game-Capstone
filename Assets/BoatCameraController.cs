using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCameraController : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public GameObject player;
    public Vector3 position;
    public float boatfastness = 1.0f;
    private Vector3 boatSpeed;
    public float direction = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = position + player.transform.position;
        transform.LookAt(player.transform);
    }

    // Update is called once per frame
    void Update()
    {

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        transform.RotateAround(player.transform.position, Vector3.up, h*Time.deltaTime);
        direction += (h * Time.deltaTime + 360);
         direction = direction % 360;
        
        Debug.Log(transform.position - player.transform.position);
        Debug.Log("Direction: "+ direction);
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = player.transform.position - transform.position;
            Vector3 speed = new Vector3(direction.x, 0, direction.z);
            boatSpeed = speed.normalized * boatfastness;
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
        }
        player.transform.position += boatSpeed;
        transform.position += boatSpeed;


    }
}
