using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgySlash : MonoBehaviour, IAbilityBase
{
    private float cdRemain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool Use()
    {
        return true;
    }
    public float CooldownLeft()
    {
        return cdRemain;
    }
}
