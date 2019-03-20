using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    public float time = 10;

    void Start()
    {
        GetComponent<Image>().canvasRenderer.SetAlpha(0.01f);
        GetComponent<Image>().CrossFadeAlpha(1, time, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
