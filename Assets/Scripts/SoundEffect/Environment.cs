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

    public bool InBossFight;

    private GenerateIsland generateIsland;
    // Start is called before the first frame update

    void Start()
    {
        audiodata = GetComponent<AudioSource>();
        generateIsland = GetComponent<GenerateIsland>();

        this.Play();
    }

    void Update()
    {
        if (InBossFight && !isPlaying)
        {
            audiodata.clip = boss_fight;

            audiodata.Play();

            isPlaying = true;
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
        Debug.Log("themeID " + generateIsland.themeID);
        if (generateIsland.themeID == 0)
        {
            audiodata.clip = forest_wind;
            //Debug.Log("森林");
        }

        else if (generateIsland.themeID == 1)
        {
            audiodata.clip = dersert;
            //Debug.Log("沙漠");
        }

        else if (generateIsland.themeID == 2)
        {
            audiodata.clip = snowstorm;
            //Debug.Log("雪地");
        }

        else if (generateIsland.themeID == 3)
        {
            audiodata.clip = swamp;
            //Debug.Log("沼泽");
        }

    }

}