using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : ScriptableObject
{
    public List<CrewMember> selectedCrew;
    public List<CrewMember> fullCrew;
    public List<CrewMember> Sacrifices;
        // Start is called before the first frame update
    public void grantCrewBonuses()
    {
        foreach (CrewMember member in selectedCrew)
        {
            member.grantBonus();
        }
    }
    //public void 
}
