using CCC.Inputs;
using UnityEngine;

[RequireComponent(typeof(StatBlock))]
[RequireComponent(typeof(PlayerClass))]
[RequireComponent(typeof(MousePositionDetector))]
public class CorruptedEarth : AbilityBase
{
    public override void UpdateStats()
    {
        throw new System.NotImplementedException();
    }

    protected override void Activate()
    {
        throw new System.NotImplementedException();
    }

    // creates an AOE around the player, causing physical damage to enemies within (long term AOE)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
