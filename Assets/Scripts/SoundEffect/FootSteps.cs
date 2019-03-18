using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    private AudioSource audiodata;
    public AudioClip[] footSteps;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayOneStep()
    {
        audiodata.clip = RandomClip();
        audiodata.Play();
    }

    AudioClip RandomClip()
    {
        return footSteps[Random.Range(0, footSteps.Length)];
    }
}