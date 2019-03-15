using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerClass))]
public class TestPlayerClass : MonoBehaviour
{
    PlayerClass pClass;
    int index;
    public ClassPrototype ClassL;

    // Start is called before the first frame update
    void Start()
    {
        pClass = GetComponent<PlayerClass>();

        pClass.allPerks = ClassL.Perks;
        pClass.onLevelUp = ClassL.OnLevel;
        pClass.TakeDefaults(ClassL.Defaults);
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
