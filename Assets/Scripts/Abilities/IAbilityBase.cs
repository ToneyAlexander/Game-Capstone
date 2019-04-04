using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityBase
{
    bool Use();
    void UpdateStats();
    void Callback(float dmgTaken);
}
