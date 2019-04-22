using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[CreateAssetMenu(menuName = "Buffs/Affliction")]
[System.Serializable]
public class Affliction : ScriptableObject
{
    // Start is called before the first frame update
    public Sprite Icon
    {
        get { return icon; }
    }

    public string AfflictionName
    {
        get { return afflictionName; }
    }

    public List<Stat> Stats
    {
        get { return stats; }
    }

    [SerializeField]
    private List<StatSlotEntry> statSlots;
    [SerializeField]
    private string afflictionName;
    [SerializeField]
    private Sprite icon;
    private List<Stat> stats;

    private void OnEnable()
    {
        stats = new List<Stat>();
        if (statSlots != null)
        {
            foreach (StatSlotEntry entry in statSlots)
            {
                Stat stat = new Stat(entry.StatName, entry.Value);
                stats.Add(stat);
            }
        }
    }

    [System.Serializable]
    private class StatSlotEntry
    {
        public string StatName
        {
            get { return statIdentifier.InternalStatName; }
        }

        public float Value
        {
            get { return value; }
        }

        [SerializeField]
        private StatIdentifier statIdentifier;

        [SerializeField]
        private float value;
    }
}
