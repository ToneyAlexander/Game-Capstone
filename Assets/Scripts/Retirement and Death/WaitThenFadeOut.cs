using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitThenFadeOut : MonoBehaviour
{
    [SerializeField]
    public float time = 1f;

    public float WaitTime = .5f;

    void Start()
    {
        //GetComponent<Image>().canvasRenderer.SetAlpha(1f);
        StartCoroutine(WaitToFade());
    }

    void Update()
    {
        if (GetComponent<Image>().canvasRenderer.GetAlpha() < .1f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator WaitToFade()
    {
        //Waits for 8 seconds before executing to show the logoState off
        yield return new WaitForSeconds(WaitTime);

        GetComponent<Image>().CrossFadeAlpha(0f, time, false);
    }
}
