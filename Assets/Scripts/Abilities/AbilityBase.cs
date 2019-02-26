using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour, IAbilityBase
{
    protected Ability abil;
    protected float cdBase;

    public abstract void UpdateStats();

    public bool Use()
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

    protected abstract void Activate();

    void Update()
    {
        if (abil.cdRemain > 0f)
        {
            abil.cdRemain -= Time.deltaTime;
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
