using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
public class HardCodedStatInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StatBlock stats = GetComponent<StatBlock>();

        stats.HealthBase = 1000f;
        stats.HealthMult = 0.26f;
        stats.HealthCur = 500f;
        stats.HealthRegen = 4f;
        stats.HealthRegenMult = 0.5f;
        stats.Armor = 145f;
        stats.ArmorMult = 0.43f;
        stats.MagicRes = 125f;
        stats.MagicResMult = 0.47f;
    }
}
