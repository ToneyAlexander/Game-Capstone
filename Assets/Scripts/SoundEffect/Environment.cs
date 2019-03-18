using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public AudioClip forest_wind;
    public AudioClip boss_fight;
    private AudioSource audiodata;
    public bool InBossFight;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayOneStep()
    {
        if (InBossFight)
        {
            audiodata.clip = boss_fight;
        }
        else
        {
            audiodata.clip = forest_wind;
        }

        audiodata.Play();
    }

}