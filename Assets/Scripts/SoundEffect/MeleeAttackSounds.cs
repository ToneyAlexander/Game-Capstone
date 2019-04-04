using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MeleeAttackSounds : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip MeleeA1;

    [SerializeField]
    private AudioClip MeleeA2;

    [SerializeField]
    private AudioClip MeleeA3;

    [SerializeField]
    private AudioClip RainOfDeath;

    [SerializeField]
    private AudioClip DaggerThrow;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayMeleeA1()
    {
        audiodata.clip = MeleeA1;
        audiodata.Play();
    }

    void PlayMeleeA2()
    {
        audiodata.clip = MeleeA2;
        audiodata.Play();
    }

    void PlayMeleeA3()
    {
        audiodata.clip = MeleeA3;
        audiodata.Play();
    }

    void PlayRainOfDeath()
    {
        audiodata.clip = RainOfDeath;
        audiodata.Play();
    }

    void PlayDaggerThrow()
    {
        audiodata.clip = DaggerThrow;
        audiodata.Play();
    }
}