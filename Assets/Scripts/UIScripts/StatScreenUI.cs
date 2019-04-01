using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenUI : MonoBehaviour
{
    public ControlStatBlock stats;
    private Text statlist;
    private Text column1;
    private Text column2;
    private Text column3;

    private Text levelText; 

    private Text staticMainStats;

    private Text classText;

    private Text nameText;

    private PlayerClass player;

    private TestPlayerClass textPlayerClass;


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
            else if (transform.GetChild(j).name.Equals("LevelText"))
            {
                levelText = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
            else if (transform.GetChild(j).name.Equals("ClassText"))
            {
                classText = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
            else if (transform.GetChild(j).name.Equals("NameText"))
            {
                nameText = transform.GetChild(j).gameObject.GetComponent<Text>();

            }
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        if (gameObjects.Length > 0)
        {
            player = gameObjects[0].GetComponent<PlayerClass>();
            textPlayerClass = gameObjects[0].GetComponent<TestPlayerClass>();
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

        statlist.text = StatBlock.CalcMult(stats.Str,stats.StrMult) + "\n" + StatBlock.CalcMult(stats.Dex, stats.DexMult) + "\n" + StatBlock.CalcMult(stats.Myst,stats.MystMult) + "\n" + StatBlock.CalcMult(stats.Fort,stats.FortMult);
        StatBlock detailedStats = stats.getStats();
        column1.text = detailedStats.HealthMax + "\n" + StatBlock.CalcMult(detailedStats.HealthRegen, detailedStats.HealthRegenMult) + "\n" + StatBlock.CalcMult(detailedStats.Armor, detailedStats.ArmorMult) + "\n" + StatBlock.CalcMult(detailedStats.MoveSpeed, detailedStats.MoveSpeedMult) + "\n" + StatBlock.CalcMult(detailedStats.AttackSpeed, detailedStats.AttackSpeedMult);
        column2.text = StatBlock.CalcMult(detailedStats.Cdr,detailedStats.CdrMult) + "\n" + StatBlock.CalcMult(detailedStats.Spell, detailedStats.SpellMult) + "\n" + StatBlock.CalcMult(detailedStats.MagicRes, detailedStats.MagicResMult) + "\n" + StatBlock.CalcMult(detailedStats.StatusRec, detailedStats.StatusRecMult) + "\n" + StatBlock.CalcMult(detailedStats.AfflictRes, detailedStats.AfflictResMult);
        column3.text = StatBlock.CalcMult(detailedStats.RangedAttack, detailedStats.RangedAttackMult) + "\n" + StatBlock.CalcMult(detailedStats.MeleeAttack, detailedStats.MeleeAttackMult) + "\n" + StatBlock.CalcMult(detailedStats.Damage, detailedStats.DamageMult) + "\n" + StatBlock.CalcMult(detailedStats.CritDamage,detailedStats.CritDamageMult) + "\n" + StatBlock.CalcMult(detailedStats.CritChance,detailedStats.CritChanceMult);
        levelText.text = player.Level.ToString();
        nameText.text = player.name.ToString();
        classText.text = textPlayerClass.ClassL.name;
    }
}
