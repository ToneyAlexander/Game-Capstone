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


}