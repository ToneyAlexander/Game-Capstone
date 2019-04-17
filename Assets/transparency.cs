using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparency : MonoBehaviour
{

    void OnTriggerEnter(Collider h)
    {
        Renderer rend = h.gameObject.GetComponent<Renderer>();
        // Debug.Log(h.transform.gameObject);
        //            Debug.Log(h.transform.position);
        if (rend)
        {
            /*
             Color col = rend.material.color;
            col.a = 0.3f;
            rend.material.transparency = 0.3f;


*/
            Debug.Log("Current Tree: " + h.transform.gameObject);
            rend.material.SetFloat("_Transparency", 0.3f);
            //  Debug.Log("Hit!");


        }
    }
    void OnTriggerExit(Collider h)
    {
        Renderer rend = h.gameObject.GetComponent<Renderer>();
        // Debug.Log(h.transform.gameObject);
        //            Debug.Log(h.transform.position);
        if (rend)
        {
            /*
             Color col = rend.material.color;
            col.a = 0.3f;
            rend.material.transparency = 0.3f;


*/
            Debug.Log("Current Tree: " + h.transform.gameObject);
            rend.material.SetFloat("_Transparency", 1.0f);
            //  Debug.Log("Hit!");


        }
    }
}
