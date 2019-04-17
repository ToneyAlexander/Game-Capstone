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
    public Sprite icon;
    
    public TimedBuff(TimedBuffPrototype tbp)
    {
        BuffName = tbp.BuffName;
        Stats = new List<Stat>();
        foreach (Stat s in tbp.Stats)
        {
            Stats.Add(new Stat(s.Name, s.Value));
        }
        icon = tbp.Icon;
        IsUnique = tbp.IsUnique;
        Duration = tbp.Duration;
        DurationLeft = Duration;
    }

    private TimedBuff()
    {

    }

    public TimedBuff ShallowClone()
    {
        TimedBuff tb = new TimedBuff
        {
            BuffName = BuffName,
            Duration = Duration,
            DurationLeft = DurationLeft,
            IsUnique = IsUnique,
            Stats = Stats
        };

        return tb;
    }

    public bool Equals(TimedBuff other)
    {
        return BuffName.Equals(other.BuffName);
    }
}
