using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MagicAttackSounds : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip MagicVolley;

    [SerializeField]
    private AudioClip MagicIgnite;

    [SerializeField]
    private AudioClip MagicAOE;

    [SerializeField]
    private AudioClip[] FireballHit;


    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlaymagicVolley()
    {
        audiodata.clip = MagicVolley;
        audiodata.Play();
    }

    void PlayMagicIgnite()
    {
        audiodata.clip = MagicIgnite;
        audiodata.Play();
    }

    void PlayMagicAOE()
    {
        audiodata.clip = MagicAOE;
        audiodata.Play();
    }

    void PlayFireballHit()
    {
        audiodata.clip = RandomClip();
        audiodata.Play();
    }

    AudioClip RandomClip()
    {
        return FireballHit[Random.Range(0, FireballHit.Length)];

    }


}