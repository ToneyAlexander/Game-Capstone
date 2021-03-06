﻿using System;
using System.Collections.Generic;

public class Damage
{
    public float physicalDmg;
    public float physicalDmgReal;
    public float magicDmg;
    public float magicDmgReal;
    public bool rangedAttack;
    public bool meleeAttack;
    public bool spell;
    public List<TimedBuff> buffs;
    public IAbilityBase callback;

    public Damage(float pD, float mD, bool ranged, bool melee, bool spl, IAbilityBase callback = null)
    {
        physicalDmg = physicalDmgReal = pD;
        magicDmg = magicDmgReal = mD;
        rangedAttack = ranged;
        meleeAttack = melee;
        spell = spl;
        buffs = new List<TimedBuff>();
        this.callback = callback;
    }
}
