using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GenerateIsland))]
[RequireComponent(typeof(AudioSource))]
public class Environment : MonoBehaviour
{
    [SerializeField]
    private AudioClip forest_wind;
    [SerializeField]
    private AudioClip dersert;
    [SerializeField]
    private AudioClip snowstorm;
    [SerializeField]
    private AudioClip swamp;

    [SerializeField]
    private AudioClip boss_fight;

    private AudioSource audiodata;
    private bool isPlaying;

    private bool isPlayArena;

    public bool InBossFight;

    private int themeID;

    private GenerateIsland generateIsland;
    // Start is called before the first frame update


    void Start()
    {
        themeID = -1;
        audiodata = GetComponent<AudioSource>();
        generateIsland = GetComponent<GenerateIsland>();
    }

    void Update()
    {
        if (generateIsland.Done()) {
            if (themeID != generateIsland.themeID)
            {
                themeID = generateIsland.themeID;
                Play();
            }
        }

        if (InBossFight && !isPlayArena)
        {
            audiodata.clip = boss_fight;

            audiodata.Play();

            isPlayArena = true;
        }
    }

    void Play()
    {
        //Debug.Log("播放");
        ThemeCheck();
        audiodata.Play();

    }

    void ThemeCheck()
    {
        Debug.Log("themeID " + themeID);
        if (themeID == 0)
        {
            audiodata.clip = forest_wind;
            //Debug.Log("森林");
        }

        else if (themeID == 1)
        {
            audiodata.clip = dersert;
            //Debug.Log("沙漠");
        }

        else if (themeID == 2)
        {
            audiodata.clip = snowstorm;
            //Debug.Log("雪地");
        }

        else if (themeID == 3)
        {
            audiodata.clip = swamp;
            //Debug.Log("沼泽");
        }

    }

}