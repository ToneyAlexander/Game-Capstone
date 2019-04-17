using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    public float time = 5;

    [SerializeField]
    public float holdTime = 5f;

    private int state = 0;

    private float midtime;

    void Start()
    {
        DontDestroyOnLoad(this.transform.parent);
        GetComponent<Image>().canvasRenderer.SetAlpha(0f);
        GetComponent<Image>().CrossFadeAlpha(1, time, false);
        midtime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0 && Time.time - midtime > holdTime + time)
        {
            state = 1;
        }
        else if (state == 1 && GetComponent<Image>().canvasRenderer.GetAlpha() > .99)
        {
            GetComponent<Image>().canvasRenderer.SetAlpha(1f);
            GetComponent<Image>().CrossFadeAlpha(0f, time, false);
            state = 2;
        }
        else if(state == 2)
        {
            GetComponent<Image>().canvasRenderer.SetAlpha(0f);
            Destroy(this);
        }
    }
}
