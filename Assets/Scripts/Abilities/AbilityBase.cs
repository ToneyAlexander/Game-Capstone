using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour, IAbilityBase
{
    protected Ability abil;
    protected float cdBase;
    protected StatBlock stats;

    public abstract void UpdateStats();

    public virtual bool Use()
    {
        if (abil.cdRemain <= 0.0001f)
        {
            abil.cdRemain = cdBase;
            Activate();
            return true;
        }
        //else
        return false;
    }

    public virtual void Callback(float dmgTaken)
    {
        //override if your ability needs a callback at some point
        Debug.LogError("Ability with no callback function specified recieved a callback.");
    }

    protected abstract void Activate();

    void Update()
    {
        if (abil.cdRemain > 0f)
        {
            float mult = 1;
            if (stats != null)
            {
                mult += StatBlock.CalcMult(stats.Cdr, stats.CdrMult);
                if (abil.isAttack)
                {
                    mult += StatBlock.CalcMult(stats.AttackSpeed,stats.AttackSpeedMult);
                }
            }
            abil.cdRemain -= Time.deltaTime * mult;
        }
        if (abil.use)
        {
            abil.use = false;
            Use();
        }
        if (abil.update)
        {
            abil.update = false;
            UpdateStats();
        }
    }
}
