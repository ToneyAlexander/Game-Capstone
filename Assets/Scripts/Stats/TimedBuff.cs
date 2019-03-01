using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using CCC.Stats;

public class TimedBuff : IEquatable<TimedBuff>
{
    public string BuffName;
    public List<Stat> Stats;
    public float Duration;
    public float DurationLeft;
    public bool IsUnique;
    
    public TimedBuff(TimedBuffPrototype tbp)
    {
        BuffName = tbp.BuffName;
        Stats = tbp.Stats;
        IsUnique = tbp.IsUnique;
        Duration = tbp.Duration;
        DurationLeft = Duration;
    }

    private TimedBuff()
    {

    }

    public TimedBuff ShallowClone()
    {
        TimedBuff tb = new TimedBuff();

        tb.BuffName = BuffName;
        tb.Duration = Duration;
        tb.DurationLeft = DurationLeft;
        tb.IsUnique = IsUnique;
        tb.Stats = Stats;

        return tb;
    }

    public bool Equals(TimedBuff other)
    {
        return BuffName.Equals(other.BuffName);
    }
}
