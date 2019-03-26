using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewController : ScriptableObject
{
    public struct CrewMemberInstance
    {
        public CrewMember cMember;
        public int currentExp;
        public int level;

    }
    public List<CrewMember> selectedCrew;
    public List<CrewMember> fullCrew;
    public List<CrewMember> Sacrifices;
    // Start is called before the first frame update
    public void grantCrewBonuses()
    {
        selectedCrew[0].grantHeadBonus();
        foreach (CrewMember member in selectedCrew)
        {
            member.grantBonus();
        }
    }
    public void levelUp()
    {
        int exp = 0;

    }

}
