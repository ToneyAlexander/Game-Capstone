using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerClass))]
public class TestPlayerClass : MonoBehaviour
{
    PlayerClass pClass;
    int index;
    public ClassPrototype ClassL;
    List<PerkPrototype> perks;

    // Start is called before the first frame update
    void Start()
    {
        pClass = GetComponent<PlayerClass>();
        perks = ClassL.Perks;

        pClass.allPerks = perks;
        //List<Perk> testPerkSet = new List<Perk>
        //{
        //    new Perk(),
        //    new Perk()
        //};
        //Perk p = new Perk();
        //List<Perk> up = new List<Perk>
        //{
        //    p
        //};
        //testPerkSet.Add(new Perk(up));

        //pClass.allPerks = testPerkSet;
        index = 0;
        Debug.Log("Init: " + index + ", " + pClass.allPerks.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Pressed L " + index + ", " + pClass.allPerks.Count);
            if(index < pClass.allPerks.Count)
            {
                bool succ = pClass.TakePerk(pClass.allPerks[index]);
                if(succ)
                {
                    Debug.Log("Success on Take Perk " + index);
                    ++index;
                } else
                {
                    Debug.Log("Fail on Take Perk " + index);
                }
            }
        }
    }
    public PlayerClass GetClass()
    {
        return pClass;
    }
}
