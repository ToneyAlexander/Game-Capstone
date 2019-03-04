using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSW3 : MonoBehaviour {
    public Material[] mats;
    Renderer rend;
    int nextMat = 0;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        
                
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextMaterial();
        }
	}

    void nextMaterial()
    {
        rend.sharedMaterial = mats[nextMat];
        if (nextMat < mats.Length-1)
        {
            nextMat += 1;
        }
        else
        {
            nextMat = 0;
        }
    }
}
