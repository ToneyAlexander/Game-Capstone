using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAbilities : MonoBehaviour
{

    public TimedBuffPrototype ignite;
    public TimedBuffPrototype dash;
    public TimedBuffPrototype mending;
    public TimedBuffPrototype hemoMend;
    public TimedBuffPrototype ablazeOff;
    public TimedBuffPrototype ablazeDef;
    public TimedBuffPrototype ablazeIgnite;
    public TimedBuffPrototype ooze;
    public TimedBuffPrototype crippled;
    public TimedBuffPrototype ripperFrenzy;
    public TimedBuffPrototype bloodBlock;
    public PerkPrototype Young;
    public PerkPrototype Middle;
    public PerkPrototype Old;

    // Start is called before the first frame update
    void Start()
    {
        FireballIgnite.ignite = ignite;
        DashAbility.Dash = dash;
        MendingAbility.Mending = mending;
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
