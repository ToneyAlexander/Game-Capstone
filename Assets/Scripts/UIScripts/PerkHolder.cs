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
    void recheck(PerkPrototype other)
    {
       if (other == perkInfo)
        {
            taken = true;
        }
       if (other.Blocks.Contains(perkInfo)){
            blocked = true;
        }
       if (other.Children.Contains(perkInfo))
        {

        }
    }
}
