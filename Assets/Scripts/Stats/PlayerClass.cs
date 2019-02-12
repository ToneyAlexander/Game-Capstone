using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControlStatBlock))]
public class PlayerClass : MonoBehaviour
{
    public List<PerkPrototype> allPerks;
    public List<PerkPrototype> takenPerks;
    private ControlStatBlock stats;

    public static bool CheckPrereq(PerkPrototype p, List<PerkPrototype> taken)
    {
        int matched = 0;
        foreach (PerkPrototype t in taken)
        {
            if (p.BlockedBy.Contains(t))
            {
                return false;
            }
            if (p.Require.Contains(t))
            {
                ++matched;
            }
        }
        if (p.RequireAll)
        {
            return matched >= p.Require.Count;
        }
        else
        {
            int req = p.Require.Count > 0 ? 1 : 0;
            return matched >= req;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        takenPerks = new List<PerkPrototype>();
        stats = GetComponent<ControlStatBlock>();
    }

    public bool TakePerk(PerkPrototype p)
    {
        if(CheckPrereq(p, takenPerks))
        {
            takenPerks.Add(p);
            stats.StatsChanged();
            return true;
        } else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
