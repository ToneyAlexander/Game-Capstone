﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHolder : MonoBehaviour
{
    public PerkPrototype perkInfo;
    public bool taken;
    public bool available;
    public bool blocked;
    public PlayerClass playerClass;
    // Start is called before the first frame update
    void Awake()
    {
        taken = false;
        available = false;
        blocked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setStartingState()
    {
        if (perkInfo.Require.Count == 0)
        {
            available = true;
        }
    }
    public void recheck()
    {
       if (!taken && !blocked)
        {
            if (playerClass.TakenPerks.Contains(perkInfo))
            {
                Debug.Log("working");
                taken = true;
            }
            
            //TODO finish this;
        }
    }
    public Color precheck(PerkPrototype other)
    {
        Color ret = Color.white;
        if (other == perkInfo || taken)
        {
            ret = Color.green;
        }
        else if (other.Blocks.Contains(perkInfo))
        {
            ret = Color.red;
        }
        else if (blocked)
        {
            ret = Color.red;
        }
        else if (available)
        {
            ret = Color.yellow;
        }
        else if (!perkInfo.RequireAll)
        {
            if (other.Children.Contains(perkInfo))
            {
                ret = Color.yellow;
            }
        }
        else if (perkInfo.RequireAll)
        {
            bool req = true;
            foreach (PerkPrototype p in perkInfo.Require)
            {
                if (!(p == other || playerClass.TakenPerks.Contains(p)))
                {
                    req = false;
                }
            }
            if (req)
            {
                ret = Color.yellow;
            }
        }
        return ret;
    }

    public void recheck(PerkPrototype other)
    {
       
       if (other == perkInfo || taken)
        {
            taken = true;
        }
       else if (other.Blocks.Contains(perkInfo)){
            blocked = true;
       }
       else if (!perkInfo.RequireAll && !available && !blocked)
        {
            if (other.Children.Contains(perkInfo))
            {
                available = true;
            }
        }
       else if (perkInfo.RequireAll && !available && !blocked)
        {
            bool req = true;
            foreach (PerkPrototype p in perkInfo.Require)
            {
                if (!(p == other || playerClass.TakenPerks.Contains(p)))
                {
                    req = false;
                }
            }
            available = req;
        }
    }
}
