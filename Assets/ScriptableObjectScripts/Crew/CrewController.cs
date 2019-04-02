using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

[CreateAssetMenu(menuName = "CrewController")]
public class CrewController : ScriptableObject
{
    public struct CrewMemberInstance
    {
        public CrewMember cMember;
        public int currentExp;
        public int level;

    }
    public int maxCrew = 3;
    public int totalCrew = 8;
    public List<CrewMember> selectedCrew;
    public List<CrewMember> fullCrew;

    public List<CrewMember> AllCrewMembers;
    public int[] themeChances = { 255, 255, 255, 255 };
    public int[] bossChances = { 255, 255 };
    public int areaBonus = 0;
    public int levelBonus = 0;
    private int selectedSlot = 999;



    // Start is called before the first frame update
    public void grantCrewBonuses()
    {
        
        foreach (CrewMember member in selectedCrew)
        {
            switch (member.CType)
            {
                case CrewMember.crewType.Navigator:
                    themeChances[member.Type] += member.Power;
                    break;
                case CrewMember.crewType.Hunter:
                    bossChances[member.Type] += member.Power;
                    break;
                case CrewMember.crewType.Naturalist:
                    areaBonus += member.Power / 100;
                    break;
                case CrewMember.crewType.Explorer:
                    levelBonus += member.Power / 100;
                    break;
            }
        }
    }
   public void fillCrew()
    {
        for (int i = 0; i < totalCrew; i++)
        {

        }
    }
    public void dismiss(int i)
    {
        selectedCrew[i].Active = false;
        selectedCrew.RemoveAt(i);
    }
   public int selectTheme()
    {
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        byte[] randomNumber = new byte[100];
        rngCsp.GetBytes(randomNumber);
        int rnd = (randomNumber[0] % themeChances.Sum());

        for (int i = 0; i < themeChances.Length; i++)
        {
            if (rnd < themeChances[i])
            {
                return i;
            }
            rnd -= themeChances[i];
        }
        return 0;
    }
    public int selectBoss()
    {
        RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        byte[] randomNumber = new byte[100];
        rngCsp.GetBytes(randomNumber);
        int rnd = (randomNumber[0] % themeChances.Sum());

        for (int i = 0; i < bossChances.Length; i++)
        {
            if (rnd < bossChances[i])
            {
                return i;
            }
            rnd -= bossChances[i];
        }
        return 0;
    }
    public void setSelectedSlot(int i)
    {
        selectedSlot =  i;
    }
    public void selectCrew(CrewMember cMember)
    {
        if (selectedSlot < maxCrew)
        {
            selectedCrew[selectedSlot] = cMember;
        }
        selectedSlot = 999;



    }
    public void levelUp()
    {
        int exp = 0;

    }

}
