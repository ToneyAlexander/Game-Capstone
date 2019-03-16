using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class RainOfDeath : AbilityBase
{
    public override void UpdateStats()
    {
        throw new System.NotImplementedException();
    }

    protected override void Activate()
    {
        throw new System.NotImplementedException();
    }

    // creates an AOE at the mouse position, sending in a brief rain of daggers

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
