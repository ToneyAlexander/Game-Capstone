using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    public List<Perk> require;
    public List<Perk> blockedBy;
    public List<Perk> children;
    public List<Perk> blocks;
    public bool requireAll;

    public static bool CheckPrereq(Perk p, List<Perk> taken)
    {
        int matched = 0;
        foreach (Perk t in taken)
        {
            if(p.blockedBy.Contains(t))
            {
                return false;
            }
            if(p.require.Contains(t))
            {
                ++matched;
            }
        }
        if(p.requireAll)
        {
            return matched >= p.require.Count;
        } else
        {
            int req = p.require.Count > 0 ? 1 : 0;
            return matched >= req;
        }
    }

    public Perk() : this(new List<Perk>())
    {
    }

    public Perk(List<Perk> require, bool requireAll = false)
    {
        this.require = require;
        blockedBy = new List<Perk>();
        children = new List<Perk>();
        blocks = new List<Perk>();
        this.requireAll = requireAll;
    }
}
