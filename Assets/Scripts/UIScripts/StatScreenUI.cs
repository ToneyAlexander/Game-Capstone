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

    private Text classAgeText;

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
            else if (transform.GetChild(j).name.Equals("ClassAge"))
            {
                classAgeText = transform.GetChild(j).gameObject.GetComponent<Text>();

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
        //Debug.Log(stats.Fort + " mult: " + stats.FortMult);
        statlist.text = StatBlock.CalcMult(stats.Str,stats.StrMult).ToString("n1") + "\n" +
            StatBlock.CalcMult(stats.Dex, stats.DexMult).ToString("n1") + "\n" + 
            StatBlock.CalcMult(stats.Myst,stats.MystMult).ToString("n1") + "\n" + 
            StatBlock.CalcMult(stats.Fort,stats.FortMult).ToString("n1");
		StatBlock detailedStats = stats.getStats();
        column1.text = detailedStats.HealthMax.ToString("n1") + "\n" + 
            StatBlock.CalcMult(detailedStats.HealthRegen, detailedStats.HealthRegenMult).ToString("n1") + "\n" + 
            StatBlock.CalcMult(detailedStats.Armor, detailedStats.ArmorMult).ToString("n1") + "\n" + 
			StatBlock.CalcMult(detailedStats.MoveSpeed, detailedStats.MoveSpeedMult).ToString("n1") + "\n" + 
			StatBlock.CalcMult(detailedStats.AttackSpeed, detailedStats.AttackSpeedMult).ToString("n1");
        column2.text = (StatBlock.CalcMult(detailedStats.Cdr,detailedStats.CdrMult) * 100).ToString("n1") + "%\n" + 
			StatBlock.CalcMult(detailedStats.Spell, detailedStats.SpellMult).ToString("n1") + "\n" + 
			StatBlock.CalcMult(detailedStats.MagicRes, detailedStats.MagicResMult).ToString("n1") + "\n" +
			(StatBlock.CalcMult(detailedStats.StatusRec, detailedStats.StatusRecMult) * 100).ToString("n1") + "%\n" + 
			(StatBlock.CalcMult(detailedStats.AfflictRes, detailedStats.AfflictResMult) * 100).ToString("n1") + "%";
        column3.text = StatBlock.CalcMult(detailedStats.RangedAttack, detailedStats.RangedAttackMult).ToString("n1") + "\n" + 
			StatBlock.CalcMult(detailedStats.MeleeAttack, detailedStats.MeleeAttackMult).ToString("n1") + "\n" + 
			StatBlock.CalcMult(detailedStats.Damage, detailedStats.DamageMult).ToString("n1") + "\n" + 
			(StatBlock.CalcMult(detailedStats.CritDamage,detailedStats.CritDamageMult) * 100).ToString("n1") + "%\n" +
			(StatBlock.CalcMult(detailedStats.CritChance,detailedStats.CritChanceMult) * 100).ToString("n1") + "%";
        levelText.text = player.PlayerLevelExp.Level.ToString();
        nameText.text = player.bloodlineController.playerName;
        classAgeText.text = textPlayerClass.ClassL.name + "\n" + player.bloodlineController.Age + " cycles";
    }
}
