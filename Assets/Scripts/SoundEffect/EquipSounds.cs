using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EquipSounds : MonoBehaviour
{
    private AudioSource audiodata;

    [SerializeField]
    private AudioClip Equip;

    [SerializeField]
    private AudioClip UnEquip;


    // Start is called before the first frame update
    void Start()
    {
        audiodata = GetComponent<AudioSource>();
    }

    void PlayEquip()
    {
        audiodata.clip = Equip;
        audiodata.Play();
    }

    void PlayUnEquip()
    {
        audiodata.clip = UnEquip;
        audiodata.Play();
    }


}