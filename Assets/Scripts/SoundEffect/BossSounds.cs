using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossSounds : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip BeetleRoar;

    [SerializeField]
    private AudioClip DemonRoar;

    [SerializeField]
    private AudioClip DragonRoar;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayBeetleRoar()
    {
        audiodata.clip = BeetleRoar;
        audiodata.Play();
    }

    void PlayDragonRoar()
    {
        audiodata.clip = DragonRoar;
        audiodata.Play();
    }

    void PlayDemonRoar()
    {
        audiodata.clip = DemonRoar;
        audiodata.Play();
    }
}
