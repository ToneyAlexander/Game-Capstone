using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ControlStatBlock))]
public class PlayerClass : MonoBehaviour
{
    public List<Perk> allPerks;
    public List<Perk> takenPerks;
    private ControlStatBlock stats;

    // Start is called before the first frame update
    void Start()
    {
        takenPerks = new List<Perk>();
        stats = GetComponent<ControlStatBlock>();
    }

    public bool TakePerk(Perk p)
    {
        if(Perk.CheckPrereq(p, takenPerks))
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
