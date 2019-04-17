using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAbilities : MonoBehaviour
{

    public TimedBuffPrototype ignite;
    public TimedBuffPrototype dash;
    public TimedBuffPrototype mending;
    public TimedBuffPrototype infusion;
    public TimedBuffPrototype corrupt;
    public TimedBuffPrototype hemoMend;
    public TimedBuffPrototype ablazeOff;
    public TimedBuffPrototype ablazeDef;
    public TimedBuffPrototype ablazeIgnite;
    public TimedBuffPrototype ooze;
    public TimedBuffPrototype crippled;
    public TimedBuffPrototype ripperFrenzy;
    public TimedBuffPrototype bloodBlock;
    public TimedBuffPrototype adrenaline;
    public PerkPrototype Young;
    public PerkPrototype Middle;
    public PerkPrototype Old;
    public AfflictionList GoodAffList;
    public AfflictionList BadAffList;

    // Start is called before the first frame update
    void Start()
    {
        FireballIgnite.ignite = ignite;
        DashAbility.Dash = dash;
        MendingAbility.Mending = mending;
        Adrenaline.AdrenalineBoost = adrenaline;
        InfusedBlade.Infusion = infusion;
        CorruptedEarth.Corruption = corrupt;
        AblazeAbility.Enemy = ablazeOff;
        AblazeAbility.Ignite = ablazeIgnite;
        AblazeAbility.Friendly = ablazeDef;
        AlienBeetle.Ooze = ooze;
        WhirlwindSlash.slowdown = crippled;
        EdgySlash.slowdown = crippled;
        RageMode.RipperFrenzy = ripperFrenzy;
        AbilityBloodRitual.Mend = hemoMend;
        AbilityBloodBlock.Block = bloodBlock;
    }
}
