﻿using System.Collections;
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
    public bool IsNegative;
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
        IsNegative = tbp.IsNegative;
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
            IsNegative = IsNegative,
            Stats = Stats,
            icon = icon
        };

        return tb;
    }

    public bool Equals(TimedBuff other)
    {
        return BuffName.Equals(other.BuffName);
    }
}
