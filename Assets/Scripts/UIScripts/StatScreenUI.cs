using System.Collections;
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
        //StatBlock.CalcMult(base,mult)

        statlist.text = "Strength: " + StatBlock.CalcMult(stats.Str,stats.StrMult) + "\nDexterity: " + StatBlock.CalcMult(stats.Dex, stats.DexMult) + "\nMysticism: " + StatBlock.CalcMult(stats.Myst,stats.MystMult) + "\nFortitude: " + StatBlock.CalcMult(stats.Fort,stats.FortMult);
        StatBlock detailedStats = stats.getStats();
        column1.text = "Health:"+ detailedStats.HealthMax + "\nHealth Regen: " + StatBlock.CalcMult(detailedStats.HealthRegen, detailedStats.HealthRegenMult) + "\nMelee Attack: " + StatBlock.CalcMult(detailedStats.MeleeAttack, detailedStats.MeleeAttackMult) + "\n\nMove Speed: " + StatBlock.CalcMult(detailedStats.MoveSpeed, detailedStats.MoveSpeedMult) + "\nAttack Speed" + StatBlock.CalcMult(detailedStats.AttackSpeed, detailedStats.AttackSpeedMult) + "\nRanged Attack: " + StatBlock.CalcMult(detailedStats.RangedAttack, detailedStats.RangedAttackMult);
        column2.text = "Cooldown Reduction Mult: " + StatBlock.CalcMult(detailedStats.Cdr,detailedStats.CdrMult) + "\nSpell: " + StatBlock.CalcMult(detailedStats.Spell, detailedStats.SpellMult) + "\n\nMagic Resistance" + StatBlock.CalcMult(detailedStats.MagicRes, detailedStats.MagicResMult) + "\nStatus Recovery" + StatBlock.CalcMult(detailedStats.StatusRec, detailedStats.StatusRecMult) + "\nAffliction Resistance: " + StatBlock.CalcMult(detailedStats.AfflictRes, detailedStats.AfflictResMult);

        column3.text = "Armour: " + StatBlock.CalcMult(detailedStats.Armor, detailedStats.ArmorMult) + "\nDamage: " + StatBlock.CalcMult(detailedStats.Damage, detailedStats.DamageMult) + "\nCrit Damage: " + StatBlock.CalcMult(detailedStats.CritDamage,detailedStats.CritDamageMult) + "\nCrit Chance: " + StatBlock.CalcMult(detailedStats.CritChance,detailedStats.CritChanceMult);
    }
}
