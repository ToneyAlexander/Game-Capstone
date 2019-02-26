using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitAbilities : MonoBehaviour
{

    public TimedBuffPrototype ignite;
    public TimedBuffPrototype dash;
    // Start is called before the first frame update
    void Start()
    {
        FireballIgnite.ignite = ignite;
        DashAbility.Dash = dash;
    }
}
