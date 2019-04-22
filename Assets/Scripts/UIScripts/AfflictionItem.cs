using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfflictionItem : MonoBehaviour
{
    private Affliction _affliction;
    public Affliction affliction
    {
        get { return _affliction; }
        set { _affliction = value; }
    }
}
