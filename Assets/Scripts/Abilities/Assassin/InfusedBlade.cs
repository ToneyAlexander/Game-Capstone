using CCC.Inputs;
using CCC.Stats;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class InfusedBlade : AbilityBase
{
    private readonly string AbilName = "Infused Blade";

    private ControlStatBlock controlStats;

    public static TimedBuffPrototype Infusion;

    private GameObject effect;

    private float critDam;

    private float critChance;

    public override void UpdateStats()
    {
        cdBase = abil.Stats.Find(item => item.Name == Stat.AS_CD).Value;
        critDam = abil.Stats.Find(item => item.Name == Stat.AS_CRITDAM).Value;
        critChance = abil.Stats.Find(item => item.Name == Stat.AS_BUFFS).Value;
    }

    protected override void Activate()
    {
        GameObject obj = Instantiate(effect, gameObject.transform.position + new Vector3(0, 1f, 0), new Quaternion());
        TimedBuff tb = Infusion.Instance;
        Stat stat = tb.Stats.Find(item => item.Name == Stat.CRIT_CHANCE);
        stat.Value += critDam;
        stat = tb.Stats.Find(item => item.Name == Stat.CRIT_DMG);
        stat.Value += critChance;
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
