using UnityEngine;

[RequireComponent(typeof(PlayerClass))]
public class TestPlayerClass : MonoBehaviour
{
    PlayerClass pClass;
    int index;
    public ClassPrototype ClassL;
    public BloodlineController BControl;
    public bool useClassSelection = true;

    #region MonoBehaviour Messages
    private void Start()
    {
        pClass = GetComponent<PlayerClass>();
        if (useClassSelection)
        {
            Debug.Log(BControl.CurrentClass);
            ClassL = BControl.CurrentClass;
        }
        pClass.allPerks = ClassL.Perks;
        pClass.onLevelUp = ClassL.OnLevel;
        pClass.TakeDefaults(ClassL.Defaults);
    }

    private void Update()
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

        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            Debug.Log("Pressed Page UP, applying 300 exp");
            pClass.ApplyExp(300);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Pressed Page DOWN, Increasing Age");
            pClass.IncreaseAge();
        }
    }
    #endregion

    public PlayerClass GetClass()
    {
        return pClass;
    }
}
