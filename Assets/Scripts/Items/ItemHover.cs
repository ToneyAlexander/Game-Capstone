using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHover : MonoBehaviour
{
    private GameObject childCanvas;

    void OnMouseOver()
    {
        childCanvas.SetActive(true);
    }

    private void OnMouseExit()
    {
        childCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        childCanvas = this.transform.GetChild(0).gameObject;
    }
}
