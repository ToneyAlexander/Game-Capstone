using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootSteps : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip[] footStepsOnGrass;

    [SerializeField]
    private AudioClip[] footStepsOnSnow;

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
        if (Environment.themeID == 0 || Environment.themeID == 3)
        {
            return footStepsOnGrass[Random.Range(0, footStepsOnGrass.Length)];
        }
        if (Environment.themeID == 2)
        {
            return footStepsOnSnow[Random.Range(0, footStepsOnGrass.Length)];
        }
        else
        {
            return footStepsOnGrass[Random.Range(0, footStepsOnGrass.Length)];
        }
    }
}