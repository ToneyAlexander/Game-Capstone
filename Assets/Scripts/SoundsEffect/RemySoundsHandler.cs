using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RemySoundsHandler : MonoBehaviour
{
    public AudioClip RunOnGrassClip;
    public AudioClip DiedClip;
    public AudioClip OneStep;

    Animator animator;
    AudioSource audiodata;
    bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audiodata = GetComponent<AudioSource>();
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isPlayingRunOnGrass()) { }
        //else if (isPlayingDied()) { }
        //else if (isPlayingMagicVolley()) { }
        //else { audiodata.clip = null; }
    }


    bool isPlayingRunOnGrass()
    {
        bool result = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Running"))
        {
            if (!isRunning) {
                audiodata.clip = RunOnGrassClip;
                audiodata.loop = true;
                audiodata.Play();
                isRunning = true;
                Debug.Log("播放跑步声");
            }
        }
        else
        {
            isRunning = false;
            audiodata.Stop();
        }
        return result;
    }

    bool isPlayingDied()
    {
        bool result = false;
        bool isPlaying = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Died"))
        {
            if (!isPlaying)
            {
                audiodata.clip = DiedClip;
                audiodata.loop = false;
                audiodata.Play();
                isPlaying = true;
                Debug.Log("播放死亡声");
                result = true;
            }
        }
        else
        {
            isPlaying = false;
        }
        return result;
    }

    bool isPlayingMagicVolley()
    {
        bool result = false;
        bool isPlaying = false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Fireball Volley"))
        {
            if (!isPlaying)
            {
                audiodata.clip = DiedClip;
                audiodata.loop = false;
                audiodata.Play();
                isPlaying = true;
                Debug.Log("播放fireball volley声");
            }
        }
        else
        {
            isPlaying = false;
        }
        return result;
    }

    void Step()
    {
        audiodata.clip = OneStep;
        audiodata.loop = false;
        audiodata.Play();
    }

}
