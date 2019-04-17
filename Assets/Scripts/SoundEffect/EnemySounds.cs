using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySounds : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip batAttack;

    [SerializeField]
    private AudioClip mushroomAttack;

    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayBatAttack()
    {
        audiodata.clip = batAttack;
        audiodata.Play();
    }

    void PlayMushroomAttack()
    {
        audiodata.clip = mushroomAttack;
        audiodata.Play();
    }
}
