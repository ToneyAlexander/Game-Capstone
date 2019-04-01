﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector3 cameraPosition;

    private Vector3[] cameras;

    int direction = 0;

    private static float shakeAmount = 0;
    private static float shakeDuration = 0;
    private static float freezeDuration = 0;
    private static float flashDuration = 0;
    public static void shake()
    {
        shakeAmount = 1.0f;
        shakeDuration = 1.0f;
    }
    public static void shake(float amount, float duration)
    {
        shakeAmount = amount;
        shakeDuration = duration;
    }


    void Start()
    {
        cameras = new Vector3[4];
        float angle = 0;
        for (int i = 0; i < cameras.Length; i++)
        {
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            cameras[i] = new Vector3(cameraPosition.x * cos - cameraPosition.z * sin, cameraPosition.y, cameraPosition.x * sin - cameraPosition.z * cos);
//            Debug.Log(cameras[i]);
            angle += Mathf.PI * 2 / cameras.Length;
        }
        Vector3 position = player.transform.position;
        position += cameras[direction];
        transform.position = position;
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
            direction = (direction - 1 + 4) % cameras.Length;
        }
        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.transform.position, 100f);
//        Debug.Log(hits.Length);
        foreach(RaycastHit h in hits)
        {
            Renderer rend = h.transform.GetComponent<Renderer>();
//            Debug.Log(h.transform.position);
            if (rend)
            {
                Color col = rend.material.color;
                col.a = 0.3f;
                rend.material.color = col;
               
            }
   
        }
        Vector3 position = player.transform.position;
        position += cameras[direction];
        transform.position = Vector3.MoveTowards(transform.position, position, 60 * Time.deltaTime) ;
        
        transform.LookAt(player.transform);
        transform.position = transform.position + Random.insideUnitSphere * shakeAmount;
        if (shakeDuration > 0)
        {
            shakeDuration -= Time.deltaTime;
        }
       
        if (shakeDuration <= 0)
        {
            shakeAmount = 0;
        }
        Vector3 offset = new Vector3();
        Vector3 mousePositionRelativetoCenter = new Vector3((Input.mousePosition.x - Screen.width / 2)/(Screen.width / 2), (Input.mousePosition.y - Screen.height / 2)/ (Screen.height / 2), Input.mousePosition.z);
//        Debug.Log(mousePositionRelativetoCenter);
        mousePositionRelativetoCenter = Vector3.ClampMagnitude(mousePositionRelativetoCenter, 1.0f);
        if (mousePositionRelativetoCenter.magnitude >= 0.1f)
        {
            transform.Rotate(-Mathf.Pow(mousePositionRelativetoCenter.y*1.5f,3.0f), Mathf.Pow(mousePositionRelativetoCenter.x * 1.5f, 3.0f), mousePositionRelativetoCenter.z);
        }


    }
}
