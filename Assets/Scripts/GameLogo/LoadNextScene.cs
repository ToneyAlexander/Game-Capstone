using CCC.GameManagement.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene : MonoBehaviour
{
    [SerializeField]
    private GameObject ScreenIn;

    [SerializeField]
    private GameObject ScreenOut;

    private float startTime;

    [SerializeField]
    private float timeToFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        //Add fade time
        timeToFadeOut += ScreenOut.GetComponentInChildren<WaitThenFadeOut>().time + ScreenOut.GetComponentInChildren<WaitThenFadeOut>().WaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeToFadeOut)
        {
            ScreenIn.SetActive(true);

            if (Time.time - startTime > timeToFadeOut + ScreenIn.GetComponentInChildren<FadeIn>().time)
            {
                GetComponent<GameStateChanger>().ChangeState();
                Destroy(this.gameObject);
            }
        }
    }
}
