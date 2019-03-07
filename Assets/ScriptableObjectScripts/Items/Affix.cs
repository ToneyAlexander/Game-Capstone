using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Items;
using CCC.Stats;

[CreateAssetMenu(menuName = "Items/Affix")]
public class Affix : ScriptableObject {
    public string affixName;
    [SerializeField]
    private List<StatPrototypeSlotEntry> statPrototypeList = new List<StatPrototypeSlotEntry>();

    [HideInInspector]
    public List<StatPrototype> statPrototypes = new List<StatPrototype>();

    private void OnEnable()
    {
        statPrototypes = new List<StatPrototype>();
        foreach (StatPrototypeSlotEntry entry in statPrototypeList)
        {
            statPrototypes.Add(new StatPrototype(entry.StatName,
                entry.MinValue, entry.MaxValue));
        }
    }
}
