﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenUI : MonoBehaviour
{
    public ControlStatBlock stats;
    Text statlist;
    Text column1;
    Text column2;
    Text column3;
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).name.Equals("MainStats"))
            {
                statlist = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
            else if (transform.GetChild(j).name.Equals("Column1"))
            {
                column1 = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
            else if (transform.GetChild(j).name.Equals("Column2"))
            {
                column2 = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
            else if (transform.GetChild(j).name.Equals("Column3"))
            {
                column3 = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            ControlStatBlock temp = gameObjects[0].GetComponent<ControlStatBlock>();
            if (temp)
            {
                stats = temp;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
       // Text statlist = gameObject.GetComponentInChildren<Text>();
        //Debug.Log(statlist);
        
        statlist.text = "Strength: " + stats.Str + "\nDexterity: " + stats.Dex + "\nMysticism: " + stats.Myst + "\nFortitude: " + stats.Fort;
        StatBlock detailedStats = stats.getStats();
        column1.text = "Health:"+ detailedStats.HealthBase + "\nHealth Regen: " + detailedStats.HealthRegen + "\nMelee Attack: " + detailedStats.MeleeAttack + "\n\nMove Speed: " + detailedStats.MoveSpeed + "\nAttack Speed" + detailedStats.MoveSpeed + "\nRanged Attack: " + detailedStats.RangedAttack;
        column2.text = "Cooldown Reduction Mult: " + detailedStats.CdrMult + "\nSpell: " + detailedStats.Spell + "\n\nMagic Resistance" + detailedStats.MagicRes + "\nStatus Recovery" + detailedStats.StatusRec + "\nAffliction Resistance: " + +detailedStats.AfflictRes;

        column3.text = "Armour: " + detailedStats.Armor + "\nDamage: " + detailedStats.Damage + "\nCrit Damage: " + detailedStats.CritDamage + "\nCrit Chance: " + detailedStats.CritChance;
    }
}
