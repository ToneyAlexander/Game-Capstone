using CCC.Inputs;
using CCC.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class Adrenaline : AbilityBase
{

    private readonly string AbilName = "Adrenaline Boost";

    private ControlStatBlock controlStats;

    public static TimedBuffPrototype AdrenalineBoost;

    private GameObject effect;

    public override void UpdateStats()
    {
        cdBase = abil.Stats.Find(item => item.Name == Stat.AS_CD).Value;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(effect, gameObject.transform.position + new Vector3(0, 2f, 0), new Quaternion());
        TimedBuff tb = AdrenalineBoost.Instance;
        controlStats.ApplyBuff(tb);
    }

    // infuse your daggers with poison - giving a boost to your damage for XX seconds

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatBlock>();
        controlStats = GetComponent<ControlStatBlock>();
        PlayerClass pc = GetComponent<PlayerClass>();
        abil = pc.abilities.Set[AbilName];
        abil.cdRemain = 0f;
        effect = abil.Prefab;
        UpdateStats();
    }
}
