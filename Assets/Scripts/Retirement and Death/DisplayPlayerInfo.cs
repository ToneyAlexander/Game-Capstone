﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerInfo : MonoBehaviour
{

    [SerializeField]
    private BloodlineController blood;

    [SerializeField]
    private LevelExpStore info;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text =
            blood.playerName + "\n" +
            (16 + blood.Age * 5) + " Years Old\n" +
            blood.CurrentClass.name + "\n" +
            "Level " + info.Level + "\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
