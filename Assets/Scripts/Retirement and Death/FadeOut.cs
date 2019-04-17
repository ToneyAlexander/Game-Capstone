using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    public float time = 10;

    void Start()
    {
        //GetComponent<Image>().canvasRenderer.SetAlpha(1f);
        GetComponent<Image>().CrossFadeAlpha(0f, time, false);
    }

    void Update()
    {
        if(GetComponent<Image>().canvasRenderer.GetAlpha() < .1f)
        {
            Destroy(gameObject);
        }
    }
}
