using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class TimedBuff
{
    public string BuffName;
    public List<Stat> Stats;
    public float Duration;
    public float DurationLeft;
    
    public TimedBuff(TimedBuffPrototype tbp)
    {
        BuffName = tbp.BuffName;
        Stats = tbp.Stats;
        Duration = tbp.Duration;
        DurationLeft = Duration;
    }
}
