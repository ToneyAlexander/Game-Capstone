using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[CreateAssetMenu]
public class TimedBuffPrototype : ScriptableObject
{
    public string BuffName
    {
        get { return buffName; }
    }

    public List<Stat> Stats
    {
        get { return stats; }
    }
    public float Duration
    {
        get { return duration; }
    }

    public TimedBuff Instance
    {
        get { return new TimedBuff(this); }
    }

    public bool IsUnique
    {
        get { return isUnique; }
    }

    [SerializeField]
    private List<StatSlotEntry> statSlots;
    [SerializeField]
    private string buffName;
    [SerializeField]
    private float duration;
    [SerializeField]
    private bool isUnique;
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
