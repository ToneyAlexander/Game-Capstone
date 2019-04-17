using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    public float time = 5;

    private bool done = false;

    void Start()
    {
        GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        GetComponent<Image>().CrossFadeAlpha(1, time, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Image>().canvasRenderer.GetAlpha() > .99)
        {
            GetComponent<Image>().canvasRenderer.SetAlpha(1f);
            done = true;
            Destroy(this.transform.parent);
        }
    }

    public bool Done()
    {
        return done;
    }
}
