using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.Stats;

public class Perk
{
    public List<Perk> Require;
    public List<Perk> BlockedBy;
    public List<Perk> Children;
    public List<Perk> Blocks;
    public bool RequireAll;
    public List<Stat> Stats;
    public string Name;


    public Perk() : this(new List<Perk>())
    {
    }

    public Perk(List<Perk> require, bool requireAll = false)
    {
        this.Require = require;
        BlockedBy = new List<Perk>();
        Children = new List<Perk>();
        Blocks = new List<Perk>();
        this.RequireAll = requireAll;
    }
}
