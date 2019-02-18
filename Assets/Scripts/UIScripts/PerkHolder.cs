using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHolder : MonoBehaviour
{
    public PerkPrototype perkInfo;
    public bool taken = false;
    public bool available = false;
    public bool blocked = false;
    public PlayerClass playerClass;
    // Start is called before the first frame update
    void Start()
    {
        
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
    void recheck()
    {
       if (!taken && !blocked)
        {
            if (playerClass.takenPerks.Contains(perkInfo))
            {
                taken = true;
            }
            
            //TODO finish this;
        }
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
                if (!(p == other || playerClass.takenPerks.Contains(p)))
                {
                    req = false;
                }
            }
            available = req;
        }
    }
}
