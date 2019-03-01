using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAbilities : MonoBehaviour
{

    public TimedBuffPrototype ignite;
    public TimedBuffPrototype dash;
    public TimedBuffPrototype mending;
    public TimedBuffPrototype ablazeOff;
    public TimedBuffPrototype ablazeDef;
    // Start is called before the first frame update
    void Start()
    {
        FireballIgnite.ignite = ignite;
        DashAbility.Dash = dash;
        MendingAbility.Mending = mending;
        AblazeAbility.Enemy = ablazeOff;
        AblazeAbility.Ignite = ignite;
        AblazeAbility.Friendly = ablazeDef;
    }
}
