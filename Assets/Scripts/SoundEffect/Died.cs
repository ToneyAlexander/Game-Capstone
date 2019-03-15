using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Died: MonoBehaviour
{
    private AudioSource audiodata;
    public AudioClip DiedClip;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayDied()
    {
        audiodata.clip = DiedClip;
        audiodata.Play();
    }
}