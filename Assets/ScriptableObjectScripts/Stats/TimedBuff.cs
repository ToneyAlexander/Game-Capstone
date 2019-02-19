using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

[CreateAssetMenu]
public class TimedBuff : ScriptableObject
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
    public float DurationLeft
    {
        get { return durationLeft; }
        set { durationLeft = value; }
    }

    [SerializeField]
    private List<StatSlotEntry> statSlots;
    [SerializeField]
    private string buffName;
    [SerializeField]
    private float duration;
    private float durationLeft;
    private List<Stat> stats;

    private void OnEnable()
    {
        durationLeft = duration;
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
